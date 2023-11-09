using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineStore.IdentityService.BLL.Exceptions;
using OnlineStore.IdentityService.BLL.Models;
using OnlineStore.IdentityService.BLL.Services.Contracts;
using OnlineStore.IdentityService.BLL.Settings;
using OnlineStore.IdentityService.BLL.Validators;
using OnlineStore.IdentityService.DAL.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.IdentityService.BLL.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwtSettings;
        private readonly ITokenService _tokenService;

        public UserAuthenticationService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginModel model)
        {
            var validator = new LoginModelValidator();
            await validator.ValidateAndThrowAsync(model);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null
                && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.CreateToken(authClaims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return new AuthenticationResult
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                };
            }
            
            throw new UnauthorizedAccessException();
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var validator = new RegisterModelValidator();
            await validator.ValidateAndThrowAsync(model);

            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                throw new EntityAlreadyExistsException("User already exists!");
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                throw new Exception("User creation failed! Please check user details and try again.");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        public async Task RegisterAdminAsync(RegisterModel model)
        {
            var validator = new RegisterModelValidator();
            await validator.ValidateAndThrowAsync(model);

            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                throw new EntityAlreadyExistsException("Admin already exists!");
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Admin creation failed! Please check admin details and try again.");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(TokenModel model)
        {
            var validator = new TokenModelValidator();
            await validator.ValidateAndThrowAsync(model);

            var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal == null)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            var username = principal?.Identity?.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null
                || user.RefreshToken != model.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            var newAccessToken = _tokenService.CreateToken(principal!.Claims.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthenticationResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };
        }

        public async Task RevokeAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new Exception("Invalid user name");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task RevokeAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}

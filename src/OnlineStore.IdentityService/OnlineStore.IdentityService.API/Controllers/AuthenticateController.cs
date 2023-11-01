using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.IdentityService.API.Models;
using OnlineStore.IdentityService.BLL.Models;
using OnlineStore.IdentityService.BLL.Services.Contracts;
using OnlineStore.IdentityService.DAL.Data;
using System.Net;

namespace OnlineStore.IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthenticateController(
            IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _userAuthenticationService.LoginAsync(model);

            return Ok(loginResult);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            await _userAuthenticationService.RegisterAsync(model);

            return Ok(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User created successfully!"
            });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            await _userAuthenticationService.RegisterAdminAsync(model);

            return Ok(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Admin created successfully!"
            });
        }   

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var refreshTokenResult = await _userAuthenticationService.RefreshTokenAsync(model);

            return Ok(refreshTokenResult);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _userAuthenticationService.RevokeAsync(username);

            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _userAuthenticationService.RevokeAllAsync();

            return NoContent();
        }
    }
}

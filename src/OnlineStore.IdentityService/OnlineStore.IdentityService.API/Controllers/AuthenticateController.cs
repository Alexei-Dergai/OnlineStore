using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.IdentityService.API.Models;
using OnlineStore.IdentityService.BLL.Services.Contracts;

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
            try
            {
                var loginResult = await _userAuthenticationService.Login(model.UserName, model.Password);

                return Ok(loginResult);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _userAuthenticationService.Register(model.UserName, model.Email, model.Password);

                return Ok(new Response
                {
                    Status = "Success",
                    Message = "User created successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                await _userAuthenticationService.RegisterAdmin(model.UserName, model.Email, model.Password);

                return Ok(new Response
                {
                    Status = "Success",
                    Message = "Admin created successfully!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest("Invalid client request");
            }

            try
            {
                var refreshTokenResult = await _userAuthenticationService.RefreshToken(tokenModel.AccessToken, tokenModel.RefreshToken);

                return Ok(refreshTokenResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            try
            {
                await _userAuthenticationService.Revoke(username);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _userAuthenticationService.RevokeAll();

            return Ok();
        }
    }
}

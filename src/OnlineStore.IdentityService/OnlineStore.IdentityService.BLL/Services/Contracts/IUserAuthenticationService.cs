using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Services.Contracts
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(LoginModel model);

        Task RegisterAsync(RegisterModel model);

        Task RegisterAdminAsync(RegisterModel model);

        Task RevokeAsync(string username);

        Task RevokeAllAsync();

        Task<AuthenticationResult> RefreshTokenAsync(TokenModel model);
    }
}

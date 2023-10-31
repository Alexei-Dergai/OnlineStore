using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Services.Contracts
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string? userName, string? password);

        Task RegisterAsync(string? userName, string? password, string? email);

        Task RegisterAdminAsync(string? userName, string? email, string? password);

        Task RevokeAsync(string username);

        Task RevokeAllAsync();

        Task<AuthenticationResult> RefreshTokenAsync(string? accessToken, string? refreshToken);
    }
}

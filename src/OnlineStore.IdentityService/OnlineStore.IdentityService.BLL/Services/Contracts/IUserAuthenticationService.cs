using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Services.Contracts
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationResult> Login(string? userName, string? password);

        Task Register(string? userName, string? password, string? email);

        Task RegisterAdmin(string? userName, string? email, string? password);

        Task Revoke(string username);

        Task RevokeAll();

        Task<AuthenticationResult> RefreshToken(string? accessToken, string? refreshToken);
    }
}

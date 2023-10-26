using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.IdentityService.BLL.Services.Contracts
{
    public interface ITokenService
    {
        JwtSecurityToken CreateToken(List<Claim> authClaims);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}

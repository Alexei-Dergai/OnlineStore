using FluentValidation;
using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Validators
{
    public class TokenModelValidator : AbstractValidator<TokenModel>
    {
        public TokenModelValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage("Access Token is required");
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh Token is required");
        }
    }
}

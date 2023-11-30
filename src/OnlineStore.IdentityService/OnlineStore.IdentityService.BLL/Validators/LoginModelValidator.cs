using FluentValidation;
using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}

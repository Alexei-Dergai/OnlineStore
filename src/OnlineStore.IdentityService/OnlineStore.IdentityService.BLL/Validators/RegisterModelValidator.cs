using FluentValidation;
using OnlineStore.IdentityService.BLL.Models;

namespace OnlineStore.IdentityService.BLL.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}

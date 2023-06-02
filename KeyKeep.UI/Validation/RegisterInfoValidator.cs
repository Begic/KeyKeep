using FluentValidation;
using KeyKeep.Data.Models;

namespace KeyKeep.UI.Validation
{
    public class RegisterInfoValidator : AbstractValidator<RegisterInfo>
    {
        public RegisterInfoValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.LoginPassword).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}

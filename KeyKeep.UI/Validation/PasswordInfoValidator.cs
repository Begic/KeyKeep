using FluentValidation;
using KeyKeep.Data.Models;

namespace KeyKeep.UI.Validation
{
    public class PasswordInfoValidator : AbstractValidator<PasswordInfo>
    {
        public PasswordInfoValidator()
        {
            RuleFor(x => x.URL).NotNull().NotEmpty();
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.UserPassword).NotNull().NotEmpty();
        }
    }
}

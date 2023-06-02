using FluentValidation;
using KeyKeep.Data.Models;

namespace KeyKeep.UI.Validation;

public class LoginInfoValidator : AbstractValidator<LoginInfo>
{
    public LoginInfoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}
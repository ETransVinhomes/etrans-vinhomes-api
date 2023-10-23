using Auth.Services.ViewModels.AuthRequestDTO;
using FluentValidation;

namespace ETransVinhomes.AuthAPI.Validations.UserValidations;
public class LoginValidation : AbstractValidator<LoginRequestDTO>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid Email");
    }
}
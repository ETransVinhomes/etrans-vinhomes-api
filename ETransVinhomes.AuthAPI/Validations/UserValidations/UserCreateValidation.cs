using System.Text.RegularExpressions;
using Auth.Services.ViewModels.AuthRequestDTO;
using FluentValidation;

namespace ETransVinhomes.AuthAPI.Validations.UserValidations;
public class UserCreateValidation : AbstractValidator<RegisterDTO>
{
    public UserCreateValidation()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid").WithErrorCode("400");
        RuleFor(x => x.PhoneNumber).NotEmpty()
       .NotNull().WithMessage("Phone Number is required.")
       .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
       .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
       .WithMessage("PhoneNumber not valid");
    }
}
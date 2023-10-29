using FluentValidation;
using Services.ViewModels.DriverModels;

namespace ETransVinhomesAPI.Validations.DriverValidations;
public class DriverUpdateValidator : AbstractValidator<DriverUpdateModel>
{
    public DriverUpdateValidator()
    {
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty()
            .MinimumLength(9)
            .MaximumLength(12).WithMessage("Phone number is not valid");
        RuleFor(x => x.DateOfBirth).NotNull()
      .NotEmpty()
      .GreaterThan(DateTime.UtcNow.AddYears(-18))
      .WithMessage("DOB is not valid");
    }
}
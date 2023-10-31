using FluentValidation;
using Services.ViewModels.CustomerModels;

namespace ETransVinhomesAPI.Validations.CustomerValidations;
public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateModel>
{
    public CustomerUpdateValidator()
    {
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty()
            .MinimumLength(9)
            .MaximumLength(12).WithMessage("Phone number is not valid");
        RuleFor(x => x.DateOfBirth).NotNull()
            .NotEmpty() 
            .LessThan(DateTime.UtcNow.AddYears(-12))
            .WithMessage("DOB is not valid");
    }
}
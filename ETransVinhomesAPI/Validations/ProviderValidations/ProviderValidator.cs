using FluentValidation;
using Services.ViewModels.ProviderModels;

namespace ETransVinhomesAPI.Validations.ProviderValidations;
public class ProviderUpdateValidator : AbstractValidator<ProviderUpdateModel>
{
    public ProviderUpdateValidator()
    {   
         RuleFor(x => x.PhoneNumber).NotNull().NotEmpty()
            .MinimumLength(9)
            .MaximumLength(12).WithMessage("Phone number is not valid");
        RuleFor(x => x.Address).NotNull().NotEmpty()
            .WithMessage("Address required!");
        
    }
}
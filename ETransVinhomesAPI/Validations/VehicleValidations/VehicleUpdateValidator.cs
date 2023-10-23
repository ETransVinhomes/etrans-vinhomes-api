using FluentValidation;
using Services.ViewModels.VehicleModels;

namespace ETransVinhomesAPI.Validations.VehicleValidationsl;
public class VehicleUpdateValidator : AbstractValidator<VehicleUpdateModel>
{
    public VehicleUpdateValidator()
    {
        RuleFor(x => x.TotalSeat).NotNull().NotEmpty().GreaterThan(1).WithMessage("Total Seat Must larger than 1");
        
    }
}
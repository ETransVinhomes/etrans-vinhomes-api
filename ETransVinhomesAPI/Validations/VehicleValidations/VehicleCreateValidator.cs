using System.Data;
using FluentValidation;
using Services.ViewModels.VehicleModels;

namespace ETransVinhomesAPI.Validations.VehicleValidations;
public class VehicleCreateValidator : AbstractValidator<VehicleCreateModel>
{
    public VehicleCreateValidator()
    {
        RuleFor(x => x.TotalSeat).NotNull().NotEmpty().GreaterThan(1).WithMessage("Total Seat must larger than 1");
        
    }
}
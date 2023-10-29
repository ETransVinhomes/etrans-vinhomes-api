using System.Data;
using FluentValidation;
using Services.ViewModels.TripModes;

namespace ETransVinhomesAPI.Validations.TripValidations;
public class TripCreateValidator : AbstractValidator<TripCreateModel>
{
    public TripCreateValidator()
    {
        RuleFor(x => x.StartedDate).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Started Date must larger than now!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must Greater Than 0");
    }
}
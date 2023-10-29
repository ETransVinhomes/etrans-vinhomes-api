using FluentValidation;
using Services.ViewModels.TicketModels;

namespace ETransVinhomesAPI.Validations.TicketValidations;
public class TicketCreateValidation : AbstractValidator<TicketCreateModel>
{
    public TicketCreateValidation()
    {
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Ticket Quantity Must Larger than 0");
    }
}
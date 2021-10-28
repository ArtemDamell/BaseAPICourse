using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    public class Ticket_EventDateAfterEnteredDateValidator : ValidationAttribute
    {
        // 40.1
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (ticket is null)
                return new ValidationResult("Ticket cannot be null.");

            if (!ticket.ValidateEventDateAfterEnteredDate())
                return new ValidationResult("Event Date has to be after Entered Date.");

            return ValidationResult.Success;
        }
    }
}

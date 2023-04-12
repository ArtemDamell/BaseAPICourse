using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    public class Ticket_EventDatePresentceValidator : ValidationAttribute
    {
        /// <summary>
        /// Validates the ticket object to ensure that the event date is present.
        /// </summary>
        /// <returns>ValidationResult.Success if the ticket is valid, otherwise a ValidationResult with an error message.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (ticket == null)
                return new ValidationResult("Ticket can't be null.");

            if (!ticket.ValidateEventDatePresence())
                return new ValidationResult("Event Date is required.");

            return ValidationResult.Success;
        }
    }
}

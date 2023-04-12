using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    public class Ticket_EventDateAfterEnteredDateValidator : ValidationAttribute
    {
        /// <summary>
        /// Validates that the Event Date of a Ticket is after the Entered Date.
        /// </summary>
        /// <returns>ValidationResult.Success if the Event Date is after the Entered Date, otherwise a ValidationResult with an error message.</returns>
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

using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    internal class Ticket_FutureDateOnCreationValidator : ValidationAttribute
    {
        /// <summary>
        /// Validates the future event date of a ticket.
        /// </summary>
        /// <returns>ValidationResult.Success if the event date is in the future, otherwise a ValidationResult with an error message.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (ticket == null)
                return new ValidationResult("Ticket can't be null.");

            if (!ticket.ValidateFutureEventDate())
                return new ValidationResult("Event Date has to be in the future.");

            return ValidationResult.Success;
        }
    }
}

using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    public class Ticket_EnteredDatePresentceValidator : ValidationAttribute
    {
        /// <summary>
        /// Validates the entered date presence for a ticket.
        /// </summary>
        /// <returns>ValidationResult.Success if the entered date is present, otherwise a ValidationResult with an error message.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if (ticket == null)
                return new ValidationResult("Ticket can't be null.");

            if (!ticket.ValidateEnteredDatePresence())
                return new ValidationResult("Entered Date is required.");

            return ValidationResult.Success;
        }
    }
}

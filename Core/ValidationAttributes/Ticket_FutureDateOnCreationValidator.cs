using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    // 38
    internal class Ticket_FutureDateOnCreationValidator : ValidationAttribute
    {
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

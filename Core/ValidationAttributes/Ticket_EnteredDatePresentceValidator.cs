using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    // 39
    public class Ticket_EnteredDatePresentceValidator : ValidationAttribute
    {
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

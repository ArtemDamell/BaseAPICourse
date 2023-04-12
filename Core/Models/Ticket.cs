using Core.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string? Owner { get; set; }

        [Ticket_EnteredDatePresentceValidator]
        public DateTime? EnteredDate { get; set; }

        [Ticket_EventDatePresentceValidator]
        [Ticket_FutureDateOnCreationValidator]
        [Ticket_EventDateAfterEnteredDateValidator]
        public DateTime? EventDate { get; set; }
        public Project? Project { get; set; }

        /// <summary>
        /// When creating a ticket, if event date is entered, it has to be in the future
        /// </summary>
        /// <returns>If valid - true, else - false</returns>
        public bool ValidateFutureEventDate()
        {
            if (!Id.Equals(0))
                return true;

            if (!EventDate.HasValue)
                return true;

            return (EventDate.Value > DateTime.Now);
        }

        /// <summary>
        /// When owner is assigned, the Entered Date has to be present
        /// </summary>
        /// <returns>If valid - true, else - false</returns>
        public bool ValidateEnteredDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner))
                return true;
            return EnteredDate.HasValue;
        }

        /// <summary>
        /// When owner is assigned, the Event Date has to be present
        /// </summary>
        /// <returns>If valid - true, else - false</returns>
        public bool ValidateEventDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner))
                return true;
            return EventDate.HasValue;
        }

        /// <summary>
        /// Validates that the EventDate is after the EnteredDate. 
        /// </summary>
        /// <returns>True if EventDate is after EnteredDate, false otherwise.</returns>
        public bool ValidateEventDateAfterEnteredDate()
        {
            if (!EventDate.HasValue || !EnteredDate.HasValue)
                return true;
            return EventDate.Value.Date >= EnteredDate.Value.Date;
        }

        /// <summary>
        /// Validates the Description property to ensure it is not null or whitespace.
        /// </summary>
        /// <returns>True if the Description property is not null or whitespace, false otherwise.</returns>
        public bool ValidateDescription() => !string.IsNullOrWhiteSpace(Description);
    }
}

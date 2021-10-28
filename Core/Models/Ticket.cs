using Core.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    // 35. Скопировать класс Ticket
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

        // 41. Добавляем свой кастомный атрибут
        [Ticket_EnteredDatePresentceValidator]
        public DateTime? EnteredDate { get; set; }

        // 41. Добавляем свой кастомный атрибут
        [Ticket_EventDatePresentceValidator]
        [Ticket_FutureDateOnCreationValidator]
        [Ticket_EventDateAfterEnteredDateValidator]
        public DateTime? EventDate { get; set; }
        public Project Project { get; set; }

        // 36.1 Создаём метод валидации проверки, в будущем ли дата события
        /// <summary>
        /// When creating a ticket, if event date is entered, it has to be in the future
        /// </summary>
        /// <returns>If valid - true, else - false</returns>
        public bool ValidateFutureEventDate()
        {
            // 36.2 Проверяем, если Ticket ID имеет значение, значит мы не
            // Не создаём а редактируем! Значит остальное валидировать не надо
            if (!Id.Equals(0))
                return true;

            // 36.3 Если ещё нет даты события, то модель валидна
            if (!EventDate.HasValue)
                return true;

            // 36.4 Если же дата есть и ID равен 0, Проверяем дату
            return (EventDate.Value > DateTime.Now);
        }

        // 36.5 Создаём метод валидации присутствия даты входа, если у билета есть владелец
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

        // 36.6 Создаём метод валидации присутствия даты Начала, если у билета есть владелец
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

        // ЭТО ПРИМЕР!!!! Создаём метод валидации даты Начала, 
        public bool ValidateEventDateAfterEnteredDate()
        {
            if (!EventDate.HasValue || !EnteredDate.HasValue)
                return true;
            return EventDate.Value.Date >= EnteredDate.Value.Date;
        }
    }
}

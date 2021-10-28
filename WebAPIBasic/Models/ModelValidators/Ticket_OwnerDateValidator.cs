//using System.ComponentModel.DataAnnotations;

//namespace WebAPIBasic.Models.ModelValidators
//{
//    // 20.1 Класс должен быть унаследован от ValidationAttribute
//    public class Ticket_OwnerDateValidator : ValidationAttribute
//    {
//        // 20.2 Переопределить базовый класс результата валидации ValidationResult
//        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//        {
//            // 20.3 Получаем для проверки объект Ticket (t.k. именно для этого класса наша аннотация создаётся)
//            var ticket = validationContext.ObjectInstance as Ticket;

//            // 20.4 Проверяем полученный Ticket на null и сразу проверим наше условие,
//            // Если Owner у нас не NULL, то и дата должна быть
//            if (ticket is not null && !string.IsNullOrWhiteSpace(ticket.Owner))
//            {
//                // 20.5 Проверяем свойство EventDate на наличие значения
//                // И если значения нет, возвращаем ошибку валидации
//                if (!ticket.EventDate.HasValue)
//                    return new ValidationResult("Due date is required when the ticket has an owner");
//            }

//            // 20.6 Если же все проверки пройдены, то возвращаем результат, как удачное прохождение
//            return ValidationResult.Success;
//        }
//    }
//}

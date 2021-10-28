//using System.ComponentModel.DataAnnotations;

//namespace WebAPIBasic.Models.ModelValidators
//{
//    // 22.1 Наследуем функционал базового класса ValidationAttribute
//    public class Ticket_FutureDateValidator : ValidationAttribute
//    {
//        //22.2 Переопределяем метод валидации базового класса
//        /* 
//            Главная проблема данного валидатора состоит в том, чтобы определить,
//            Создаётся ли объект или обновляется. Т.к. при обновлении мы НЕ ДОЛЖНЫ
//            Применять эту валидацию.
//         */
//        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//        {
//            // 22.3 Получаем для проверки и типизируем объект Ticket
//            var ticket = validationContext.ObjectInstance as Ticket;

//            // 20.4 Проверяем полученный Ticket на null и сразу проверим наше условие,
//            // Если EventDate у нас не NULL, то и дата должна быть в будущем
//            // И в то же самое время проверяем ID билета, именно он нам ответит на
//            // Вопрос, создаётся ли билет, или обновляется! Когда билет создаётся
//            // Его ID будет 0, либо null, если ID типа int?
//            if (ticket is not null && (ticket.Id.Equals(0) || ticket.Id.Equals(null)))
//            {
//                // 20.5 Проверяем дату и если она не в будущем - возвращаем ошибку валидации
//                if (ticket.EventDate.HasValue && ticket.EventDate <= DateTime.UtcNow)
//                    return new ValidationResult("Date has to be in the future!");
//            }

//            // 20.6 Возвращаем положительный результат валидации
//            return ValidationResult.Success;
//        }
//    }
//}

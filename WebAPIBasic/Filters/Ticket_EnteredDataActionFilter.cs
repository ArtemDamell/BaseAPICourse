//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using WebAPIBasic.Models;

//namespace WebAPIBasic.Filters
//{
//    // 25.1 Мы должны наследовать весь функционал от базового класса ActionFilterAttribute
//    public class Ticket_EnteredDataActionFilter : ActionFilterAttribute
//    {
//        // 25.2
//        /*
//            У нас есть ряд методов для переопределения, в данной ситуации нас
//            Интересует метод OnActionExecuting, он отвечает за логику, которая
//            Должна сработать ДО выполнения метода обработки в контроллере
//         */
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            base.OnActionExecuting(context);
//            // 25.3
//            /*
//                Для доступа к данным модели, передаваемой в параметре метода
//                У нас имеется входной параметер context
//                Воспользуемся им для получения информации о переданном билете
//             */
//            var ticket = context.ActionArguments["ticket"] as Ticket;
//            // После этого добавить новое свойство в модель Ticket - Entered Date
//            // 25.5 Проверяем, билет на null и если свойство EnteredDate не имеет информации
//            if (ticket is not null && !string.IsNullOrWhiteSpace(ticket.Owner) && !ticket.EnteredDate.HasValue)
//            {
//                // 25.6 
//                /*
//                    Т.к. за обработку данных модели в рамках контроллера отвечает
//                    ModelState, воспользуемся им для добавления ошибки в модель валидации,
//                    Тем самым сделав её не валидной
//                 */
//                context.ModelState.AddModelError("EnteredDate", "Entered Date is required.");

//                // 25.7 Возвращаем результат в конвеер обработки
//                context.Result = new BadRequestObjectResult(context.ModelState);

//                /*
//                    После чего применяем этот валидационный фильтер к методу обработки версии 2,
//                    А не в модель, как мы привыкли.
//                 */
//            }
//        }
//    }
//}

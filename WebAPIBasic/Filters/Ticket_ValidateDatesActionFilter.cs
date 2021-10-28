//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using WebAPIBasic.Models;

//namespace WebAPIBasic.Filters
//{
//    // 30.1
//    // Изменить название класса действующего фильтра Ticket_EnteredDataActionFilter на более подходящее, т.к. его логика работы меняется
//    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            base.OnActionExecuting(context);

//            var ticket = context.ActionArguments["ticket"] as Ticket;

//            // 30.2 Разделяем логику проверок, внедряя новые критерии
//            if (ticket is not null && !string.IsNullOrWhiteSpace(ticket.Owner))
//            {
//                // 30.3 Для безопасности, вводим переменную для проверки валидности модели
//                bool isValid = true;

//                if (!ticket.EnteredDate.HasValue)
//                {
//                    context.ModelState.AddModelError("EnteredDate", "Entered Date is required.");
//                    isValid = false;
//                }

//                if (ticket.EnteredDate.HasValue && ticket.EventDate.HasValue && ticket.EnteredDate > ticket.EventDate)
//                {
//                    context.ModelState.AddModelError("EventDate", "Event Date has to be later than the Entered Date.");
//                    isValid = false;
//                }

//                if (!isValid)
//                    context.Result = new BadRequestObjectResult(context.ModelState);
//            }
//        }
//    }
//}

using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIBasic.Filters.V2
{
    // 59.1 Создаём Action Filter
    public class Ticket_EnshureDescriptionPresentActionFilterAttribute : ActionFilterAttribute
    {
        // 59.2 --> После назначения переопределения, до реализации, идём в модель Ticket и создаём в нём метод валидации
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // 59.3/59.3 Реализовываем фильтр
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null && !ticket.ValidateDescription())
            {
                context.ModelState.AddModelError("Description", "Description is required!");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}

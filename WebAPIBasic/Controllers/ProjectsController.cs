using Microsoft.AspNetCore.Mvc;


/*
    8. Проверочная работа 
 */
namespace WebAPIBasic.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the projects.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading project #{id}.");
        }

        /*
            9. Чтобы переопределить маршрут, мы обязаны в Rote атрибуте
            Указать ПОЛНЫЙ путь, начиная с корня /api
         */
        [HttpGet]
        /*
            Этот маршрут требует наличия 2-х параметров
         */
        //[Route("/api/projects/{pId}/tickets/{tId}")]

        /*
            Но есть другой варианд, передавать параметры через браузерную 
            Строку по имени
            [Route("/api/projects/{pId}/tickets")]
            Тогда адрес для передачи второго параметра будет таким
            /api/projects/{pId}/tickets?tId=57
         */
        [Route("/api/projects/{pId}/tickets")]
        public IActionResult GetProjectTicket(int pId, [FromQuery] int tId)
        {
            /*
                Если через параметр браузерной строки не передаётся значение
                tId, то .Net Core будет использовать значение по умолчанию
                Для данного типа параметра (у int это 0)
                И мы можем легко обработать данный случай
             */
            if (tId == 0)
                return Ok($"Reading all tickets belong to project #{pId}");

            return Ok($"Reading project #{pId}, ticket #{tId}");
        }

        // 11. Метод со вторым параметром модели
        // Демонстрирует передачу модели через строку браузера
        /*
            Этот вариант передачи данных крайне не рекомендуется использовать!!!
            В параметрах браузера чаще всего передают ТОЛЬКО примитивные типы,
            Такие как ID. Модели - передаём через тело.
         */
        //[Route("/api/projects/{pId}/tickets")]
        //public IActionResult GetProjectTicket([FromQuery] Ticket ticket)
        //{
        //    if (ticket == null)
        //        return BadRequest("Parameters are not properly!");

        //    if (ticket.Id == 0)
        //        return Ok($"Reading all tickets belong to project #{ticket.ProjectId}");

        //    return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.Id}, title: {ticket.Title}, description: {ticket.Description}");
        //}

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating new project.");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project #{id}.");
        }
    }
}

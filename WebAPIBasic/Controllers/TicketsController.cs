using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using WebAPIBasic.Filters;
//using WebAPIBasic.Models;


namespace WebAPIBasic.Controllers
{
    /*
        4.1 Чтобы превратить простой класс в контроллер
        Необходимо унаследовать его от класса ControllerBase
        Но, чтобы превратить его именно в API контроллер
        Необходимо добавить аннотацию [APIController]
     */
    [ApiController]
    /*
        4.3 Если стоит маршрут на уровне контроллера ([Route("api/[controller]")])
        То можно не прописывать пути к каждому методу, .NET Core будет сам сопаставлять
        Маршрут с адресом контроллера и искать соответствующий метод
     */
    [Route("api/[controller]")]

    // 28. Применяем новый фильтр на уровень контроллера
    /*
        Но это мы применяем фильтр только на конкретный контроллер! А что если
        Нам необходимо добавить этот фильтр ГЛОБАЛЬНО, на всё приложение?
        Далее добавим этот фильтр к middleware конбейееру обработки в класс Program
     */
    //[DiscontinueVersion1ResourseFilter]
    public class TicketsController : ControllerBase
    {
        // 55.1 Внедряем зависимость базы данных
        private readonly AppDbContext _db;
        public TicketsController(AppDbContext db)
        {
            _db = db;
        }

        /*
            4.2 Для маршрутизации одного класса мало, нужны конечные точки
            Ими будут служить наши методы действий, декарированные атрибутами
            [HttpGet] - тип http запроса
            [Route("api/tickets")] - конечный адрес запроса
         */
        [HttpGet]
        // 4.4 Убираем лишние пути
        //[Route("api/tickets")]
        /*
            Возвращаемый динамический тип IActionResult
            Содержит реализацию всего необходимого функционала
            Для возврата любых типов данных
         */
        public IActionResult Get()
        {
            // 55.2 Переписываем метод получения всех билетов, старая реализация закомментирована
            //return Ok("Reading all the tickets.");
            List<Ticket> allTickets = new();

            try
            {
                allTickets = _db.Tickets.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

            if (allTickets is null)
                return NotFound();

            return Ok(allTickets);
        }

        /*
            4.4 Тут у нас работа с параметром ID, поэтому, чтобы убрать маршрут
            Нам потребуется изменить сигнатуру аннотации [HttpGet]
         */
        //[HttpGet]
        [HttpGet("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult GetById(int id)
        {
            // 55.3 Переписываем метод получения всех билетов, старая реализация закомментирована
            //return Ok($"Reading ticket #{id}.");

            Ticket? ticket = new();

            try
            {
                ticket = _db.Tickets.Find(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            if (ticket is null)
                return NotFound();

            return Ok(ticket);
        }

        /* ОРИГИНАЛЬНЫЙ МЕТОД (Начальный)
        [HttpPost]
        // 4.4 Убираем лишние пути
        //[Route("api/tickets")]
        // 14.1 Добаавляем параметр Ticket для передачи объекта через тело запроса
        public IActionResult Post([FromBody] Ticket ticket)
        {
            //return Ok("Creating new ticket.");
            return Ok(ticket);
        }
        */

        // 23.1 ***************************************************************
        /*
            Представим ситуацию, когда нам надо модифицировать имеющийся метод,
            Но наш API сервис уже используется несколькими коммерчискими проектами.
            Как нам можно разработать новый метод, но так, чтобы и старый остался работать?
            Ответ очевиден, нам надо переопределить имеющийся метод с указанием версии
            И создать второй такой же, указав и его версию
         */
        [HttpPost]
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        //[HttpPost]
        // Для переопределения метода, добавляем ему маршрут с указанием версии
        //[Route("/api/v2/tickets")]
        // 25.8 Тем самым этот атрибут валидации (фильтр) применится ТОЛЬКО ко 2-ой версии метода
        /*
            Указав наш собственный фильтр мы гарантируем, что если условие не будет соблюдено,
            То данный метод выполнятся НЕ БУДЕТ, а сразу отправит ошибку 400 пользователю
         */
        //[Ticket_EnteredDataActionFilter]

        // 30.4 Поменять в TicketsController аннотации валидации
        //[Ticket_ValidateDatesActionFilter]
        //public IActionResult PostV2([FromBody] Ticket ticket)
        //{
        //    return Ok(ticket);
        //}
        // ********************************************************************

        [HttpPut]
        // 4.4 Убираем лишние пути
        //[Route("api/tickets")]
        // 14.2 Добаавляем параметр Ticket для передачи объекта через тело запроса
        public IActionResult Put([FromBody] Ticket ticket)
        {
            //return Ok("Updating a ticket.");
            return Ok(ticket);
        }

        // 4.4 Убираем лишние пути, передавая ID
        //[HttpDelete]
        [HttpDelete("{id}")]
        //[Route("api/tickets/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting ticket #{id}.");
        }
    }
}

//using Core.Models;
//using DataStore.EF;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;


///*
//	8. Проверочная работа 
// */
//namespace WebAPIBasic.Controllers
//{
//	[ApiController]
//	[Route("api/[controller]")]
//	public class ProjectsControllerOLD : ControllerBase
//	{
//		// 51.1 Создаём конструктор класса
//		private readonly AppDbContext _db;
//		public ProjectsControllerOLD(AppDbContext db)
//		{
//			_db = db;
//		}

//		[HttpGet]
//		public IActionResult Get()
//		{
//			// 52.1 Переписываем метод, оригинал закомментирован
//			//return Ok("Reading all the projects.");
//			return Ok(_db.Projects.ToList());
//		}

//		[HttpGet("{id}")]
//		public IActionResult GetById(int id)
//		{
//			// 52.2 Переписываем метод, оригинал закомментирован
//			//return Ok($"Reading project #{id}.");
//			var project = _db.Projects.Find(id);

//			if (project is null)
//				return NotFound();

//			return Ok(project);
//		}

//		/*
//			9. Чтобы переопределить маршрут, мы обязаны в Rote атрибуте
//			Указать ПОЛНЫЙ путь, начиная с корня /api
//		 */
//		[HttpGet]
//		/*
//			Этот маршрут требует наличия 2-х параметров
//		 */
//		//[Route("/api/projects/{pId}/tickets/{tId}")]

//		/*
//			Но есть другой варианд, передавать параметры через браузерную 
//			Строку по имени
//			[Route("/api/projects/{pId}/tickets")]
//			Тогда адрес для передачи второго параметра будет таким
//			/api/projects/{pId}/tickets?tId=57
//		 */
//		[Route("/api/projects/{pId}/tickets")]
//		public IActionResult GetProjectTicket(int pId/*, [FromQuery] int tId*/)
//		{
//			/*
//				Если через параметр браузерной строки не передаётся значение
//				tId, то .Net Core будет использовать значение по умолчанию
//				Для данного типа параметра (у int это 0)
//				И мы можем легко обработать данный случай
//			 */

//			// 52.3 Переписываем метод, оригинал закомментирован
//			//if (tId == 0)
//			//	return Ok($"Reading all tickets belong to project #{pId}");
//			//return Ok($"Reading project #{pId}, ticket #{tId}");
			
//			var tickets = _db.Tickets.Where(x => x.ProjectId == pId).ToList();

//			if (tickets is null || !tickets.Any())
//				return NotFound();
//			return Ok(tickets);
//		}

//		// 11. Метод со вторым параметром модели
//		// Демонстрирует передачу модели через строку браузера
//		/*
//			Этот вариант передачи данных крайне не рекомендуется использовать!!!
//			В параметрах браузера чаще всего передают ТОЛЬКО примитивные типы,
//			Такие как ID. Модели - передаём через тело.
//		 */
//		//[Route("/api/projects/{pId}/tickets")]
//		//public IActionResult GetProjectTicket([FromQuery] Ticket ticket)
//		//{
//		//    if (ticket == null)
//		//        return BadRequest("Parameters are not properly!");

//		//    if (ticket.Id == 0)
//		//        return Ok($"Reading all tickets belong to project #{ticket.ProjectId}");

//		//    return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.Id}, title: {ticket.Title}, description: {ticket.Description}");
//		//}

//		[HttpPost]
//		// 52.1 Добавляем в метод параметр модели
//		public IActionResult Post([FromBody] Project project)
//		{
//			// 53.2 Переписываем метод, оригинал закомментирован
//			//return Ok("Creating new project.");

//			_db.Projects.Add(project);
//			_db.SaveChanges();

//			// 53.3 После успешного создания переадресовываемся на метод получения информации 
//			//		О созданном проекте
//			return CreatedAtAction(nameof(GetById), 
//				new { id = project.Id },
//				project);

//		}

//		[HttpPut("{id}")]
//		// 53.4 Добавляем в метод параметр модели и ID
//		public IActionResult Put(int id, Project project)
//		{
//			// 53.5 Переписываем метод, оригинал закомментирован
//			//return Ok("Updating a project.");

//			if (!id.Equals(project.Id))
//				return BadRequest();

//			// 53.6 Рассмотрим ещё один метод обновления сущностей, кроме Update
//			_db.Entry(project).State = EntityState.Modified; // <-- Эта запись равна _db.Projects.Update(project);

//			try
//			{
//				_db.SaveChanges();
//			}
//			catch (Exception)
//			{
//				if (_db.Projects.Find(id) is null)
//					return NotFound();

//				throw;
//			}

//			return NoContent();
//		}

//		[HttpDelete("{id}")]
//		public IActionResult Delete(int id)
//		{
//			// 53.7 Переписываем метод, оригинал закомментирован
//			//return Ok($"Deleting project #{id}.");

//			var project = _db.Projects.Find(id);

//			if (project is null)
//				return NotFound();

//			_db.Projects.Remove(project);
//			_db.SaveChanges();

//			return Ok(project);
//		}
//	}
//}


// Создаём необходимые переменные, HttpClient мы можем в консольки создать
// Напрямую, но в нормальном приложении - только через внедрение зависимости
using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using static System.Console;

HttpClient httpClient = new();
IWebApiExecuter webApiExecuter = new WebApiExecuter("https://localhost:44336", httpClient);

// OWN CHANGES
WriteLine("-----------------------------------");
WriteLine("Reading all projects");

// 85.2 Вызываем созданный метод
await GetProjects();

WriteLine("Project is readed! Next step start");
WriteLine("------------------------------------");
WriteLine("Reading all tickets in project...");

await GetProjectTickets(1);

WriteLine("------------------------------------");
WriteLine("Create a new project");

var proj = await CreateProject();
await GetProjects();

WriteLine("------------------------------------");
WriteLine("Update a project");

await UpdateProject(proj);
await GetProjects();

WriteLine("------------------------------------");
WriteLine("Delete a project");

await DeleteProject(proj);
await GetProjects();


// 85.1 Создаём метод для тестирования получения всех проектов
// Предварительно добавив ссылки на проекты Core и Repository
async Task GetProjects()
{
    // 85.3 Создаём экземпляр репозитория
    ProjectRepository repository = new(webApiExecuter);

    // 85.4 Получаем все проекты
    var projects = await repository.GetAsync();

    // 85.5 Выводим на консоль все проекты
    foreach (var item in projects)
    {
        WriteLine($"Project : {item.Name}");
    }
    // После этого устанавливаем в Solution Properties проекты для запуска
}

async Task<Project> GetProject(int id)
{
    ProjectRepository repository = new(webApiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int id)
{
    var project = await GetProject(id);
    WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(webApiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(id);

    foreach (var ticket in tickets)
    {
        WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<int> CreateProject()
{
    var project = new Project { Name = "Another Project 3" };  // <-- Для теста ошибок, делаем null
    ProjectRepository repository = new(webApiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(int id)
{
    ProjectRepository repository = new(webApiExecuter);
    var project = await repository.GetByIdAsync(id);
    project.Name = " UPDATED"; // <-- Для теста ошибок, делаем null
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    ProjectRepository repository = new(webApiExecuter);
    await repository.DeleteAsync(id);
}
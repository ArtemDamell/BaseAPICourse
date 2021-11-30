
// Создаём необходимые переменные, HttpClient мы можем в консольки создать
// Напрямую, но в нормальном приложении - только через внедрение зависимости
using App.Repository;
using App.Repository.ApiClient;

HttpClient httpClient = new();
IWebApiExecuter webApiExecuter = new WebApiExecuter("https://localhost:44336", httpClient);

// 85.2 Вызываем созданный метод
await GetProjects();

// 85.1 Создаём метод для тестирования получения всех проектов
// Предварительно добавив ссылки на проекты Core и Repository
async Task GetProjects()
{
    // 85.3 Создаём экземпляр репозитория
    ProjectRepository repository = new(webApiExecuter);

    // 85.4 Получаем все проекты
    var projects = await repository.Get();

    // 85.5 Выводим на консоль все проекты
    foreach (var item in projects)
    {
        Console.WriteLine($"Project : {item.Name}");
    }
    // После этого устанавливаем в Solution Properties проекты для запуска
}
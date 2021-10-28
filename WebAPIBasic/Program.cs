/*
 Так, как класс StartUp отсутствует в .NET 6
 Все конфигурации происходят прямо тут
 */

//using WebAPIBasic.Filters;

var builder = WebApplication.CreateBuilder(args);

/*Это место специально для конфигурации зависимостей*/

// 2.2 Конфигурируем зависимость контроллеров маршрута
builder.Services.AddControllers();
// ---------------------------------------------------

// 29. Добавляем опции к нашим контроллерам и через них устанавливаем глобальный фильтр
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<DiscontinueVersion1ResourseFilter>();
//});

// 7.2 Конфигурируем swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "Web API Base Course", Version = "v1" });
});

/****************************************************/

var app = builder.Build();

// 7.1 Добавляем функционал Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Basic Course v1"));
}

// Тут у нас находится маршрут по умолчанию
/*
 2.1 Заменяем стандартную конечную точку маршрута на контроллер маршрутизации
 Но просто указать его не достаточно, его необходимо сконфигурировать
 Раньше это делалось в методе ConfigureServices класса Startup
 */
//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();

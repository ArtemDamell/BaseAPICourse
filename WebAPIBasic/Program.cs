/*
 Так, как класс StartUp отсутствует в .NET 6
 Все конфигурации происходят прямо тут
 */

//using WebAPIBasic.Filters;

using DataStore.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebAPIBasic.Swagger;
using WebAPIBasic.Auth;

var builder = WebApplication.CreateBuilder(args);

// 131. Добавить зависимости в класс Program WebAPI проекта (симулируем ситуацию, когда у нас есть только один клиент)
builder.Services.AddSingleton<ICustomTokenManager, CustomTokenManager>();
builder.Services.AddSingleton<ICustomUserManager, CustomUserManager>();


/*Это место специально для конфигурации зависимостей*/

// 2.2 Конфигурируем зависимость контроллеров маршрута
builder.Services.AddControllers();
// ---------------------------------------------------

// 29. Добавляем опции к нашим контроллерам и через них устанавливаем глобальный фильтр
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<DiscontinueVersion1ResourseFilter>();
//});

// 61. Конфигурируем библиотеку Versioning
builder.Services.AddApiVersioning(options =>
{
    // Версия по умолчанию
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Возвращаем в ответе использованную версию API
    options.ReportApiVersions = true;

    // Настройка для поиска версии в заголовках запроса
    // 64. Закомментировать в классе Pogram метод указания заголовка для версии
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

// 73. Внедрить зависимость AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 48 Конфигурируем EF
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 7.2 Конфигурируем swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "Web API Base Course", Version = "v1" });

    //78.2 / 78.2 Конфигурируем поддержку версий в Swagger
    x.SwaggerDoc("v2", new() { Title = "Web API Base Course", Version = "v2" });

    // 63.2/63.2 Конфигурируем заголовок для версии API
    //x.OperationFilter<CustomHeaderSwaggerAttribute>();
});

// 93.1 Для того, чтобы подружить API и WebAssambly из-за разных доменов, конфигурируем в Program проекта WebAPI AddCorse
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44359")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// 77. Конфигурируем библиотеку в классе Program
builder.Services.AddVersionedApiExplorer( options =>
{
    options.GroupNameFormat = "'v'VVV"; 
});
// 77.************************************************************

/****************************************************/

var app = builder.Build();

// 7.1 Добавляем функционал Swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Basic Course v1");
        //78.1 / 78.2 Конфигурируем поддержку версий в Swagger
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "Web API Basic Course v2");
        });
}

// 93.2 Подключаем Middleware политики
app.UseCors();

// Тут у нас находится маршрут по умолчанию
/*
 2.1 Заменяем стандартную конечную точку маршрута на контроллер маршрутизации
 Но просто указать его не достаточно, его необходимо сконфигурировать
 Раньше это делалось в методе ConfigureServices класса Startup
 */
//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();

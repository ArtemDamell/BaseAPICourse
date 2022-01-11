/*
 ���, ��� ����� StartUp ����������� � .NET 6
 ��� ������������ ���������� ����� ���
 */

//using WebAPIBasic.Filters;

using DataStore.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebAPIBasic.Swagger;

var builder = WebApplication.CreateBuilder(args);

/*��� ����� ���������� ��� ������������ ������������*/

// 2.2 ������������� ����������� ������������ ��������
builder.Services.AddControllers();
// ---------------------------------------------------

// 29. ��������� ����� � ����� ������������ � ����� ��� ������������� ���������� ������
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<DiscontinueVersion1ResourseFilter>();
//});

// 61. ������������� ���������� Versioning
builder.Services.AddApiVersioning(options =>
{
    // ������ �� ���������
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // ���������� � ������ �������������� ������ API
    options.ReportApiVersions = true;

    // ��������� ��� ������ ������ � ���������� �������
    // 64. ���������������� � ������ Pogram ����� �������� ��������� ��� ������
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

// 73. �������� ����������� AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 48 ������������� EF
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 7.2 ������������� swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "Web API Base Course", Version = "v1" });

    //78.2 / 78.2 ������������� ��������� ������ � Swagger
    x.SwaggerDoc("v2", new() { Title = "Web API Base Course", Version = "v2" });

    // 63.2/63.2 ������������� ��������� ��� ������ API
    //x.OperationFilter<CustomHeaderSwaggerAttribute>();
});

// 93.1 ��� ����, ����� ��������� API � WebAssambly ��-�� ������ �������, ������������� � Program ������� WebAPI AddCorse
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44359")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// 77. ������������� ���������� � ������ Program
builder.Services.AddVersionedApiExplorer( options =>
{
    options.GroupNameFormat = "'v'VVV"; 
});
// 77.************************************************************

/****************************************************/

var app = builder.Build();

// 7.1 ��������� ���������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Basic Course v1");
        //78.1 / 78.2 ������������� ��������� ������ � Swagger
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "Web API Basic Course v2");
        });
}

// 93.2 ���������� Middleware ��������
app.UseCors();

// ��� � ��� ��������� ������� �� ���������
/*
 2.1 �������� ����������� �������� ����� �������� �� ���������� �������������
 �� ������ ������� ��� �� ����������, ��� ���������� ����������������
 ������ ��� �������� � ������ ConfigureServices ������ Startup
 */
//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();

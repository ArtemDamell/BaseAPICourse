/*
 ���, ��� ����� StartUp ����������� � .NET 6
 ��� ������������ ���������� ����� ���
 */

//using WebAPIBasic.Filters;

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

// 7.2 ������������� swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "Web API Base Course", Version = "v1" });
});

/****************************************************/

var app = builder.Build();

// 7.1 ��������� ���������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Basic Course v1"));
}

// ��� � ��� ��������� ������� �� ���������
/*
 2.1 �������� ����������� �������� ����� �������� �� ���������� �������������
 �� ������ ������� ��� �� ����������, ��� ���������� ����������������
 ������ ��� �������� � ������ ConfigureServices ������ Startup
 */
//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();

using DataStore.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebAPIBasic.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
builder.Services.AddSingleton<ICustomUserManager, CustomUserManager>();

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer")
                       .AddJwtBearer("Bearer", options =>
                       {
                           options.Authority = "https://localhost:5001";
                           options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                           {
                               ValidateAudience = false
                           };
                       });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WebApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "webapi");
    });

    options.AddPolicy("WebApiWriteScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "write");
    });
});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new() { Title = "Web API Base Course", Version = "v1" });
    x.SwaggerDoc("v2", new() { Title = "Web API Base Course", Version = "v2" });
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44359")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddVersionedApiExplorer( options =>
{
    options.GroupNameFormat = "'v'VVV"; 
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Basic Course v1");
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "Web API Basic Course v2");
        });
}
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

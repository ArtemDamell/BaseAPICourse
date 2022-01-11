using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient()));

// 94.1 На этот раз используем метод AddTransient, чтобы для каждого http запроса создавался свой экземпляр
builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

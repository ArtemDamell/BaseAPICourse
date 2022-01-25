using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 122. В классе Program проекта WEB добавить параметер с ключём
// 125. Добавить логику в класс Program проекта MyApp.Web, после HttpClient() добавить новый параметер
// --> После этого вносим правки в WebExecuter
builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), "blazorwasm", ">bTH}A1IEC0aTx4Et?f^H>v4]Hmg[R311au[;HN)(!Z0aa-=UsHZEnF)~9yE|#u"));

// 94.1 На этот раз используем метод AddTransient, чтобы для каждого http запроса создавался свой экземпляр
builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

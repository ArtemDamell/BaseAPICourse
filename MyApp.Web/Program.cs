using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 149. ������� � Program � ���������������� CustomeTokenAuthenticationStateProvider
builder.Services.AddSingleton<AuthenticationStateProvider, CustomeTokenAuthenticationStateProvider>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// 144.1 ������������� ����� ������ � WebApiExecuter � ������ Program ������� MyApp.WEB
builder.Services.AddSingleton<ITokenRepository, TokenRepository>();

// 122. � ������ Program ������� WEB �������� ��������� � ������
// 125. �������� ������ � ����� Program ������� MyApp.Web, ����� HttpClient() �������� ����� ���������
// --> ����� ����� ������ ������ � WebExecuter
//builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), "blazorwasm", ">bTH}A1IEC0aTx4Et?f^H>v4]Hmg[R311au[;HN)(!Z0aa-=UsHZEnF)~9yE|#u"));

// 144.2
builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), options.GetRequiredService<ITokenRepository>()));
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IAuthenticationScreen, AuthenticationScreen>();


// 94.1 �� ���� ��� ���������� ����� AddTransient, ����� ��� ������� http ������� ���������� ���� ���������
builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

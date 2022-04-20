using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 149. Перейти в Program и сконфигурировать CustomeTokenAuthenticationStateProvider
// 193.1 Удалить из класса Program проекта WEB ненужные конфигурации
//builder.Services.AddOptions();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddSingleton<AuthenticationStateProvider, JwtTokenAuthenticationStateProvider>();

//builder.Services.AddSingleton<AuthenticationStateProvider, CustomeTokenAuthenticationStateProvider>();

// 144.1 Конфигурируем новый сервис и WebApiExecuter в классе Program проекта MyApp.WEB
// 202.2 Закомментировать builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
//builder.Services.AddSingleton<ITokenRepository, TokenRepository>();

// 122. В классе Program проекта WEB добавить параметер с ключём
// 125. Добавить логику в класс Program проекта MyApp.Web, после HttpClient() добавить новый параметер
// --> После этого вносим правки в WebExecuter
//builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), "blazorwasm", ">bTH}A1IEC0aTx4Et?f^H>v4]Hmg[R311au[;HN)(!Z0aa-=UsHZEnF)~9yE|#u"));

// 144.2
//builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), options.GetRequiredService<ITokenRepository>()));

// 202.1 Изменить конструктор в классе Program проекта MyApp.WEB
builder.Services.AddSingleton(x => x.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));
builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri("https://localhost:44336/"))
                                                                                              .AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddTransient<AuthorizationMessageHandler>(sp =>
{
    // Get required services
    var provider = sp.GetRequiredService<IAccessTokenProvider>();
    var navManager = sp.GetRequiredService<NavigationManager>();

    // Create new AuthorizationMessageHandler instance, configure them and return after that
    var handler = new AuthorizationMessageHandler(provider, navManager);
    handler.ConfigureHandler(authorizedUrls: new[]
    {
        // List of url's which to be attached access token
        "https://localhost:44336/"
    });
    return handler;
});
// https://localhost:44336
builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>();
// 202.1 -------------------------------------------------------
// 193.2 Удалить из класса Program проекта WEB ненужные конфигурации
//builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
//builder.Services.AddTransient<IAuthenticationScreen, AuthenticationScreen>();


// 94.1 На этот раз используем метод AddTransient, чтобы для каждого http запроса создавался свой экземпляр
builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();

// 189. Скопировать из класса Program демо проекта блок AddOidcAuthentication
builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.ProviderOptions.DefaultScopes.Add("webapi");
});


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

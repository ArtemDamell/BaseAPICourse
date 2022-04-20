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

// 149. ������� � Program � ���������������� CustomeTokenAuthenticationStateProvider
// 193.1 ������� �� ������ Program ������� WEB �������� ������������
//builder.Services.AddOptions();
//builder.Services.AddAuthorizationCore();
//builder.Services.AddSingleton<AuthenticationStateProvider, JwtTokenAuthenticationStateProvider>();

//builder.Services.AddSingleton<AuthenticationStateProvider, CustomeTokenAuthenticationStateProvider>();

// 144.1 ������������� ����� ������ � WebApiExecuter � ������ Program ������� MyApp.WEB
// 202.2 ���������������� builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
//builder.Services.AddSingleton<ITokenRepository, TokenRepository>();

// 122. � ������ Program ������� WEB �������� ��������� � ������
// 125. �������� ������ � ����� Program ������� MyApp.Web, ����� HttpClient() �������� ����� ���������
// --> ����� ����� ������ ������ � WebExecuter
//builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), "blazorwasm", ">bTH}A1IEC0aTx4Et?f^H>v4]Hmg[R311au[;HN)(!Z0aa-=UsHZEnF)~9yE|#u"));

// 144.2
//builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44336", new HttpClient(), options.GetRequiredService<ITokenRepository>()));

// 202.1 �������� ����������� � ������ Program ������� MyApp.WEB
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
// 193.2 ������� �� ������ Program ������� WEB �������� ������������
//builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
//builder.Services.AddTransient<IAuthenticationScreen, AuthenticationScreen>();


// 94.1 �� ���� ��� ���������� ����� AddTransient, ����� ��� ������� http ������� ���������� ���� ���������
builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();

// 189. ����������� �� ������ Program ���� ������� ���� AddOidcAuthentication
builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.ProviderOptions.DefaultScopes.Add("webapi");
});


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

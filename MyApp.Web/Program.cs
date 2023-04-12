using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

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

builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>();

builder.Services.AddTransient<IProjectsScreen, ProjectsScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketsScreen, TicketsScreen>();

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.ProviderOptions.DefaultScopes.Add("webapi");
});

await builder.Build().RunAsync();

// 178. Для тестирования IdentityServer'а и доступа к API, создадим новый консольный проект ConsoleClient

// 180. На этом этапе приходим в консоль
// Создаём экземпляр HttpClient для взаимодействия с нашими сервисами
using IdentityModel.Client;

var client = new HttpClient();
var disc = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disc.IsError)
{
    Console.WriteLine(disc.Error);
    return;
}

// Get the access token
var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disc.TokenEndpoint,

    ClientId = "console.client",
    ClientSecret = "secret",
    Scope = "webapi"
});

if (token.IsError)
{
    Console.WriteLine(token.Error);
    return;
}

Console.WriteLine(token.Json);
Console.ReadLine();

// 181. На этом этапе выставляем 3 проекта для запуска
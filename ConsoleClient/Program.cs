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
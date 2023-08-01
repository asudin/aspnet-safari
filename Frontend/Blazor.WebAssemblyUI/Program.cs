using Blazor.WebAssemblyUI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.WebAssemblyUI.Services;

var apiUrl = "https://localhost:7104/";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddSingleton<GameApiClient>(serviceProvider =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
    return new GameApiClient(httpClient);
});

await builder.Build().RunAsync();
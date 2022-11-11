using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TagIt.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddTagItClient()
    .ConfigureHttpClient( c => c.BaseAddress = new Uri("https://localhost:5001") )
    .ConfigureWebSocketClient(c => c.Uri = new Uri("wss://https://localhost:5001"));

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TagIt.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

UIOptions? options = builder.Configuration
    .GetSection("TagIt")
    .Get<UIOptions>();

builder.Services.AddViewModels();

builder.Services.AddTagItClient()
    .ConfigureHttpClient( c => c.BaseAddress = new Uri("https://localhost:5001/graphql")  )
    .ConfigureWebSocketClient(c => c.Uri = new Uri("wss://localhost:5001/graphql"));

await builder.Build().RunAsync();

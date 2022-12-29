using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TagIt.Configuration;

namespace TagIt.PaperlessNgx;

public static class PaperlessNgxServiceCollectionExtensions
{
    public static ITagItServerBuilder AddPaperlessNgx(this ITagItServerBuilder builder)
    {
        builder.Services.AddOptions<PaperlessNgxOptions>()
            .BindConfiguration(Config.Section + ":PaperlessNgx");

        builder.Services.AddHttpClient("Paperless")
            .ConfigureHttpClient((provider, client) =>
            {
                PaperlessNgxOptions options = provider.GetRequiredService<IOptions<PaperlessNgxOptions>>().Value;
                client.BaseAddress = new Uri(options.Url);

                if (!string.IsNullOrEmpty(options.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Token", options.Token);
                }
                else
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                $"{options.Username}:{options.Password}")));
                }
            });

        builder.Services.AddSingleton<IPaperlessDocumentClient, PaperlessPaperlessDocumentClient>();
        builder.Services.AddSingleton<IPdfOcrService, PaperlessPaperlessDocumentClient>();

        return builder;
    }
}

internal static class HttpClientFactoryExtensions
{
    internal static HttpClient CreatePaperlessNgxClient(this IHttpClientFactory factory)
    {
        return factory.CreateClient("Paperless");
    }
}

public class PaperlessNgxOptions
{
    public string Url { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Token { get; set; }
}

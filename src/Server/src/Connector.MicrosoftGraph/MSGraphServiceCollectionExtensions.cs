using Microsoft.Extensions.DependencyInjection;
using TagIt.Configuration;
using TagIt.Connectors;
using TagIt.MicrosoftGraph;

namespace TagIt;

public static class MicrosoftGraphServiceCollectionExtensions
{
    public static ITagItServerBuilder AddMicrosoftGraphConnectors(this ITagItServerBuilder builder)
    {
        builder.Services.AddSingleton<IGraphClientFactory, GraphClientFactory>();
        builder.Services.AddSingleton<IConnectionManager, MicrosoftGraphConnectionManager>();

        return builder;
    }
}

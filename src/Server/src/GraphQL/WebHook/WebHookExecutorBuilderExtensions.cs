using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;

public static class WebHookRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddWebHooks(
        this IRequestExecutorBuilder builder)
    {
        // types
        builder
            .AddTypeExtension<WebHookQueries>();

        return builder;
    }
}

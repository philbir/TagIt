using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;

public static class ConnectorRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddConnectors(
        this IRequestExecutorBuilder builder)
    {
        // types
        builder
            .AddTypeExtension<ConnectorQueries>();

        // extensions
        builder
            .AddTypeExtension<ConnectorExtensions>();

        // nodes
        builder
            .AddTypeExtension<ConnectorDefintionNode>();

        return builder;
    }
}

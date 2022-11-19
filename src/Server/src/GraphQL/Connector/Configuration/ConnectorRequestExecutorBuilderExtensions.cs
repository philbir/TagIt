using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TagIt.GraphQL.Connector.DataLoader;

namespace TagIt.GraphQL;

public static class ConnectorRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddConnectors(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDataLoader<ConnectorDefinitionByIdDataLoader>();

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

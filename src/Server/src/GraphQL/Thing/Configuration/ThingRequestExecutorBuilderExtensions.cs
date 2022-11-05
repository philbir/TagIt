using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;



public static class ThingRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddThings(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDataLoader<ThingTypeByIdDataLoader>();

        // types
        builder
            .AddTypeExtension<ThingQueries>();

        // extensions
        builder
            .AddTypeExtension<ThingExtensions>();

        // nodes
        builder
            .AddTypeExtension<ThingNode>();

        return builder;
    }
}

using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;

public static class ThingRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddThings(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDataLoader<PropertyDefinitionByIdDataLoader>()
            .AddDataLoader<ThingClassByIdDataLoader>()
            .AddDataLoader<ThingTypeByIdDataLoader>();

        // types
        builder
            .AddType<ThingGraphQLType>()
            .AddType<ThingThumbnailType>()
            .AddType<ThingClassType>()
            .AddType<ThingTypeType>()
            .AddTypeExtension<ThingQueries>()
            .AddTypeExtension<CorrespondentQueries>()
            .AddTypeExtension<CorrespondentMutations>();

        // extensions
        builder
            .AddTypeExtension<ThingExtensions>();

        // nodes
        builder
            .AddTypeExtension<CorrespondentNode>()
            .AddTypeExtension<ThingNode>();

        return builder;
    }
}

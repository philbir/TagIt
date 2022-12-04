using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;

public static class JobRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddJobs(
        this IRequestExecutorBuilder builder)
    {
        // types
        builder
            .AddTypeExtension<JobMutations>()
            .AddTypeExtension<JobQueries>();

        // extensions
        builder
            .AddType<JobDefintionType>()
            .AddType<JobActionType>();

        // nodes
        builder
            .AddTypeExtension<JobDefintionNode>();

        return builder;
    }
}

using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TagIt.GraphQL.Job.Extensions;

namespace TagIt.GraphQL;

public static class JobRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddJobs(
        this IRequestExecutorBuilder builder)
    {
        // types
        builder
            .AddTypeExtension<JobQueries>();

        // extensions
        builder
            .AddTypeExtension<JobDefinitionExtensions>()
            .AddTypeExtension<JobActionExtensions>();

        // nodes
        builder
            .AddTypeExtension<JobDefintionNode>();

        return builder;
    }
}

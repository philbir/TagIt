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
            .AddTypeExtension<JobQueries>();

        //// extensions
        //builder
        //    .AddTypeExtension<ConnectorExtensions>();

        //// nodes
        //builder
        //    .AddTypeExtension<ConnectorDefintionNode>();

        return builder;
    }
}

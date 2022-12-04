using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TagIt.Configuration;
using TagIt.GraphQL;

namespace TagIt;

public static class GraphQLServiceCollectionExtensions
{
    public static ITagItServerBuilder AddGraphQL(
        this ITagItServerBuilder builder)
    {
        builder.Services.AddGraphQLServer()
            .AddTagItSchema();

        return builder;
    }

    public static IRequestExecutorBuilder AddTagItSchema(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddThings()
            .AddCredentials()
            .AddWebHooks()
            .AddConnectors()
            .AddJobs()
            .AddMutationConventions()
            .AddErrorInterfaceType<IUserError>()
            .AddSharedTypes()
            .AddAuthorization()
            .AddGlobalObjectIdentification()
            .AddFiltering()
            .AddSorting()
            .ModifyOptions(x =>
            {
                x.EnableFlagEnums = true;
                x.EnableOneOf = true;
            });

        return builder;
    }

    private static IRequestExecutorBuilder AddSharedTypes(this IRequestExecutorBuilder builder)
    {
        builder.AddQueryType();
        builder.AddMutationType();
        builder.AddInterfaceType<IUserError>();

        return builder;
    }
}

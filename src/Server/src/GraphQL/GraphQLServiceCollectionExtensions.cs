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
            .AddTagIt()
            .AddMutationConventions()
            .AddErrorInterfaceType<IUserError>()
            .AddSharedTypes()
            .AddAuthorization()
            .AddGlobalObjectIdentification()
            .AddFiltering()
            .AddSorting()
            .AddTypeConverter<DateTimeOffset, DateTime>(t =>
                {
                    DateTime date = t.LocalDateTime;
                    DateTime.SpecifyKind(date, DateTimeKind.Utc);
                    return date;
                }
            )
            /*
            .AddTypeConverter<DateTime, DateTimeOffset>(
                t => t.Kind is DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(t, DateTimeKind.Utc)
                    : t)*/
            .ModifyOptions(x =>
            {
                x.EnableOneOf = true;
            });

        return builder;
    }

    private static IRequestExecutorBuilder AddSharedTypes(this IRequestExecutorBuilder builder)
    {
        builder.AddInterfaceType<IUserError>();
        return builder;
    }
}

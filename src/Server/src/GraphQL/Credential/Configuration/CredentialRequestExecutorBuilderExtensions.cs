using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.GraphQL;

public static class CredentialRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddCredentials(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDataLoader<CredentialByIdDataLoader>();

        // types
        builder
            .AddType<ProtectedValueType>()
            .AddType<CredentialType>()
            .AddType<CredentialTokenType>()
            .AddType<OAuthClientType>()
            .AddTypeExtension<CredentialQueries>()
            .AddTypeExtension<CredentialMutations>();

        return builder;
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Extensions.Context;
using TagIt.Configuration;

namespace TagIt.Store.Mongo;

public static class MongoStoreCollectionExtensions
{
    public static ITagItServerBuilder AddMongoStore(
        this ITagItServerBuilder builder)
    {
        builder.Services.AddOptions<MongoOptions>(nameof(TagIdDbContext))
            .BindConfiguration(Config.Storage.Database);

        builder.Services.AddSingleton<ITagIdDbContext>(p =>
            new TagIdDbContext(p.GetRequiredService<IOptionsMonitor<MongoOptions>>())
        );

        builder.Services.AddStores();

        return builder;
    }

    public static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddStore<IThingStore, Thing, ThingStore>();
        services.AddStore<IThingTypeStore, ThingType, ThingTypeStore>();
        services.AddStore<IThingClassStore, ThingClass, ThingClassStore>();
        services.AddStore<ICredentialsStore, Credential, CredentialsStore>();
        services.AddSingleton<IAuditStore, AuditStore>();
        services.AddSingleton<ICorrespendentStore, CorrespondentStore>();
        services.AddSingleton<IFileStore, FileStore>();
        services.AddSingleton<IConnectorStore, ConnectorStore>();
        services.AddSingleton<IJobDefintionStore, JobDefintionStore>();
        services.AddSingleton<IJobRunStore, JobRunStore>();
        services.AddSingleton<IWebHookStore, WebHookStore>();
        services.AddSingleton<IThingContentStore, ThingContentStore>();
        services.AddSingleton<IClientAuthStateStore, ClientAuthStateStore>();
        services.AddSingleton<IThumbnailStore, ThumbnailStore>();
        services.AddSingleton<IReceiverStore, ReceiverStore>();
        services.AddSingleton<IPropertyDefinitionStore, PropertyDefinitionStore>();
        services.AddSingleton<ITagDefinitionStore, TagDefinitionStore>();

        return services;
    }

    private static IServiceCollection AddStore<TService, TEntity, TImplementation>(
        this IServiceCollection services)
        where TService : class
        where TEntity : class, IEntity, new()
        where TImplementation : class, TService
    {
        services.AddSingleton<TService, TImplementation>();
        services.AddSingleton(typeof(IStore<TEntity>), typeof(TImplementation));

        return services;
    }
}

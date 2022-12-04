using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using TagIt.Connectors;
using TagIt.Jobs;
using TagIt.Security;
using TagIt.Store;
using TagIt.Messaging;
using TagIt.Configuration;
using TagIt.Pdf;
using TagIt.Image;

namespace TagIt;

public static class TagItServerBuilderExtensions
{
    public static ITagItServerBuilder AddTagItServer(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSectionName = "TagIt")
    {
        IConfigurationSection section = configuration.GetSection(configSectionName);
        var builder = new TagItServerBuilder(section, services);

        builder.Services.AddOptions<TagItServerOptions>(nameof(TagItServerOptions))
            .BindConfiguration(Config.Section);

        builder.Services.AddCoreServices();
        builder.Services.AddScheduler();

        return builder;
    }

    private static IServiceCollection AddCoreServices(
        this IServiceCollection services)
    {
        services.AddSingleton<ILabelGeneratorService, LabelGeneratorService>();

        //TODO: Replace with something secure
        services.AddSingleton<IValueProtector, TotalyUselessAndUnsafeValueProtector>();

        services.AddSingleton<IConnectorFactory, ConnectorFactory>();
        services.AddSingleton<IWebHookService, WebHookService>();
        services.AddSingleton<IThingIngestService, ThingIngestService>();
        services.AddSingleton<IThingDataResolver, ThingDataResolver>();
        services.AddEntityService<IThingService, ThingService, Thing>();
        services.AddEntityService<IThingTypeService, ThingTypeService, ThingType>();
        services.AddEntityService<IThingClassService, ThingClassService, ThingClass>();
        services.AddSingleton<ICredentialStoreService, CredentialStoreService>();
        services.AddSingleton<IConnectorDefinitionService, ConnectorDefinitionService>();
        services.AddSingleton<ICorrespondentService, CorrespondentService>();
        services.AddSingleton<IUserContextAccessor, UserContextAccessor>();
        services.AddSingleton<IOpenIdConnectDiscoveryService, OpenIdConnectDiscoveryService>();
        services.AddSingleton<ICredentialStoreTokenManager, CredentialStoreTokenManager>();
        services.AddSingleton<IJobDefintionService, JobDefintionService>();
        services.AddSingleton<IReceiverService, ReceiverService>();
        services.AddSingleton<ITagDefinitionService, TagDefinitionService>();
        services.AddHttpClient();

        services.AddSingleton<IImageConverter, MagickImageConverter>();
        services.AddSingleton<IThumbnailGeneratorService, ThumbnailGeneratorService>();
        services.AddPdfImageConversion();

        services.AddSingleton<IDataExtractionService, DataExtractionService>();
        services.AddSingleton<IThingDataExtractor, PdfTextDataExtractor>();

        services.AddSingleton<IConnectionManager>((sp) =>
        {
            return new DefaultConnectionManager(sp);
        });

        return services;
    }

    private static IServiceCollection AddScheduler(
        this IServiceCollection services)
    {
        services.AddSingleton<StdSchedulerFactory>();
        services.AddSingleton<SchedulerAccessor>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        services.AddSingleton<IJobFactory, SingletonJobFactory>();

        services.AddSingleton<IJobManager, JobManager>();
        services.AddSingleton<IJob, HarvesterJob>();
        services.AddSingleton<IConnectorItemIdSerializer, ConnectorItemIdSerializer>();

        services.AddSingleton<IMessagePublisher, MessagePublisher>();


        return services;
    }

    private static IServiceCollection AddEntityService<TService, TImplementation, TEntity>(
            this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
        where TEntity : class, IEntityWithVersion, new()
    {
        services.AddSingleton<TService, TImplementation>();
        services.AddSingleton<IEntityManager<TEntity>, EntityManager<TEntity>>();

        return services;
    }
}

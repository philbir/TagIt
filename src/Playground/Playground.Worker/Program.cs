using Serilog;
using TagIt;
using TagIt.Configuration;
using TagIt.Messaging;
using TagIt.Security;
using TagIt.Store.Mongo;

Logging.CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {
        ITagItServerBuilder builder = services.AddTagItServer(context.Configuration);
        builder
            .AddMongoStore()
            .AddMicrosoftGraphConnectors()
            .AddMessaging(b =>
            {
                b.AddConsumer<NewConnectorItemConsumer>();
                b.AddConsumer<ThingAddedConsumer>();
            });
        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<IUserContextFactory, UserContextFactory>();
        builder.Services.AddSingleton<DataSeeder>();
        //services.AddHostedService<TagItWorker>();
        services.AddHostedService<JobWorker>();
    })
    .UseSerilog()
    .Build();

IUserContextAccessor accessor = host.Services.GetRequiredService<IUserContextAccessor>();
accessor.Context = await host.Services.GetRequiredService<IUserContextFactory>()
    .CreateAsync(CancellationToken.None);

await host.Services.GetRequiredService<DataSeeder>().SeedAsync(CancellationToken.None);
await host.RunAsync();

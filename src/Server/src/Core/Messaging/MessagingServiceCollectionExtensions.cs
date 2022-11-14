using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TagIt.Configuration;

namespace TagIt.Messaging;

public static class MessagingServiceCollectionExtensions
{
    public static ITagItServerBuilder AddMessaging(
        this ITagItServerBuilder builder,
        Action<IBusRegistrationConfigurator>? configureBus = null)
    {
        builder.Services.AddOptions<MessagingOptions>(nameof(Config.Messaging))
            .BindConfiguration(Config.Messaging);

        MessagingOptions options = builder.Configuration
            .GetSection(nameof(Config.Messaging))
            .Get<MessagingOptions>();

        builder.Services.AddMassTransit(s =>
        {
            configureBus?.Invoke(s);

            if (options.Transport == MessagingTransport.InMemory)
            {
                s.UsingInMemory((provider, cfg) =>
                {
                    cfg.ReceiveEndpoint(options.ServiceBus?.WorkerQueueName, e =>
                    {
                        e.ConfigureConsumers(provider);
                    });
                });
            }
            if (options.Transport == MessagingTransport.RabbitMQ)
            {
                s.UsingRabbitMq((provider, cfg) =>
                {
                    cfg.Host(options.ServiceBus.Host, c =>
                    {
                        c.Username(options.ServiceBus.Username);
                        c.Password(options.ServiceBus.Password);
                    });

                    cfg.ReceiveEndpoint(options.ServiceBus?.WorkerQueueName, e =>
                    {
                        e.ConfigureConsumers(provider);
                        e.PrefetchCount = 1;
                    });
                });
            }

        });

        return builder;
    }

    private static MessagingOptions GetOptions(this IServiceProvider services)
    {
        return services
                    .GetRequiredService<IOptionsMonitor<MessagingOptions>>()
                    .Get(nameof(Config.Messaging));
    }
}

namespace TagIt.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken);
}

public class MessagingOptions
{
    public MessagingTransport Transport { get; set; }

    public ServiceBusOptions? ServiceBus { get; set; }
}

public enum MessagingTransport
{
    InMemory,
    AzureServiceBus,
    RabbitMQ
}

public class ServiceBusOptions
{
    public string? Host { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? WorkerQueueName { get; set; }
}

using MassTransit;

namespace TagIt.Messaging;

public class MessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;

    public MessagePublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));
        
        await _bus.Publish(message, cancellationToken);
    }
}

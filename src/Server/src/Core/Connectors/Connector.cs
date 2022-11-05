namespace TagIt.Connectors;

public class Connector
{
    protected readonly IConnectorItemIdSerializer _itemIdSerializer;

    public Connector(IConnectorItemIdSerializer itemIdSerializer)
    {
        _itemIdSerializer = itemIdSerializer;
    }

    public Guid Id { get; set; }

    public string Root { get; set; } = default!;

    public virtual Task InitializeAsync(
        ConnectorDefintion defintion,
        CancellationToken cancellationToken)
    {
        Id = defintion.Id;
        Root = defintion.Root;

        return Task.CompletedTask;
    }

    public virtual string GetItemId(string itemId)
    {
        return _itemIdSerializer.Serialize(itemId, Id);
    }
}
namespace TagIt.Messaging;

public record NewConnectorItemMessage(ConnectorItem Item, JobAction Action)
{
    public bool RequestItemInfo { get; set; }
}


namespace TagIt.Connectors;

public interface IConnectorItemIdSerializer
{
    ItemIdentifier Deserialize(string value);
    string Serialize(ConnectorItem item);
    string Serialize(string id, Guid connectorId);
}

public record ItemIdentifier(string id, Guid ConnectorId);

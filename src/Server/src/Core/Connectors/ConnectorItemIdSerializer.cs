using System.Text;

namespace TagIt.Connectors;

public class ConnectorItemIdSerializer : IConnectorItemIdSerializer
{
    public string Serialize(ConnectorItem item)
    {
        return Serialize(item.Id, item.ConnectorId);
    }

    public string Serialize(string id, Guid connectorId)
    {
        var data = Encoding.UTF8.GetBytes(
            string.Join('|',
            id,
            connectorId.ToString("N")));

        return Convert.ToBase64String(data);
    }

    public ItemIdentifier Deserialize(string value)
    {
        var data = Convert.FromBase64String(value);
        var parts = Encoding.UTF8.GetString(data).Split('|');

        return new ItemIdentifier(parts[0], Guid.Parse(parts[1]));
    }
}


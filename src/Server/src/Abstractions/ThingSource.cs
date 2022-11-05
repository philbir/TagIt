#nullable disable

namespace TagIt;

public class ThingSource
{
    public Guid ConnectorId { get; set; }

    public string Location { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

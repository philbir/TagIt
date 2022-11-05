#nullable disable
using TagIt.Store;

namespace TagIt;

public class Thing : EntityWithVersion, IEntityWithVersion
{
    public string Title { get; set; }

    public Guid? TypeId { get; set; }
    public Guid? ClassId { get; set; }

    public ThingState State { get; set; }

    public string Label { get; set; } = string.Empty;

    public Guid? CorespondentId { get; set; }

    public Guid? ReceiverId { get; set; }

    public IReadOnlyList<Tag> Tags { get; set; }

    public DateTime? Date { get; set; }

    public IReadOnlyList<Thumbnail> Thumbnails { get; set; } = new List<Thumbnail>();

    public IReadOnlyList<ThingRelation> Relations { get; set; } = new List<ThingRelation>();

    public IReadOnlyList<ThingDataReference> Data { get; set; } = new List<ThingDataReference>();
}

public class ThingDataReference
{
    public string Type { get; set; }

    public Guid ConnectorId { get; set; }

    public string Location { get; set; }
}

public record Thumbnail(int PageNumber, string FileId);

public class AddThingRequest
{
    public string Title { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? ClassId { get; set; }

    public string Type { get; set; }

    public string? Label { get; set; }

    public Guid? Corespondent { get; set; }

    public Guid? Receiver { get; set; }

    public IReadOnlyList<Tag> Tags { get; set; }

    public DateTime? Date { get; set; }

    public IReadOnlyList<ThingData> Data { get; set; }

    public JobAction Action { get; set; }
}

public enum ThingState
{
    Draft,
    Processing,
    Active,
    Deleted
}

public class ThingData : ThingDataReference
{
    public byte[] Data { get; set; } = new byte[0];
    public string Id { get; set; }
}

public class EmailMessage
{
    public string Subject { get; set; }

    public DateTimeOffset ReceivedAt { get; set; }

    public EmailBody Body { get; set; }

    public EmailAddress From { get; set; }

    public IReadOnlyList<EmailAddress> To { get; set; }

    public IReadOnlyList<EmailAddress> Cc { get; set; }
}

public class EmailAddress
{
    public string Address { get; set; }

    public string Name { get; set; }
}

public class EmailBody
{
    public string ContextType { get; set; }

    public string Body { get; set; }

    public string Preview { get; set; }
}

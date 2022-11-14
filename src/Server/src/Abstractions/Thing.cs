#nullable disable
using TagIt.Store;

namespace TagIt;

public class Thing : EntityWithVersion, IEntityWithVersion
{
    public string Title { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? ClassId { get; set; }

    public ThingSource Source { get; set; }

    public ThingState State { get; set; }

    public string Label { get; set; } = string.Empty;

    public Guid? CorespondentId { get; set; }

    public Guid? ReceiverId { get; set; }

    public IReadOnlyList<Tag> Tags { get; set; }

    public DateTime? Date { get; set; }

    public IReadOnlyList<ThingThumbnail> Thumbnails { get; set; } = new List<ThingThumbnail>();

    public IReadOnlyList<ThingRelation> Relations { get; set; } = new List<ThingRelation>();

    public IReadOnlyList<ThingDataReference> Data { get; set; } = new List<ThingDataReference>();
}

public class ThingDataReference
{
    public Guid ConnectorId { get; set; }

    public string Id { get; set; }

    public string Type { get; set; }

    public string ContentType { get; set; }
}

public class ThingThumbnail : ImageData
{
    public string FileId { get; set; }

    public int PageNumber { get; set; }
}

public class ImageInfo
{
    public ImageFormat Format { get; set; }

    public ImageSize Size { get; set; }
}

public class ImageData : ImageInfo
{
    public byte[] Data { get; set; }
}

public enum ImageFormat
{
    WebP,
    Png
}

public class ImageSize
{
    public int Height { get; set; }

    public int Width { get; set; }
}

public class AddThingRequest
{
    public string Title { get; set; }

    public Guid? TypeId { get; set; }

    public Guid? ClassId { get; set; }

    public string ContentType { get; set; }

    public string? Label { get; set; }

    public Guid? Corespondent { get; set; }

    public Guid? Receiver { get; set; }

    public IReadOnlyList<Tag> Tags { get; set; }

    public DateTime? Date { get; set; }

    public ThingSource Source { get; set; }

    public IReadOnlyList<ThingData> AdditionalData { get; set; }

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
    public Stream Stream { get; set; }
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

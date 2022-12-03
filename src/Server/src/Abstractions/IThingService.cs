
namespace TagIt;

public interface IThingService
{
    Task<Thing> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IQueryable<Thing>> Query(CancellationToken cancellationToken);

    Task<Thing> UpdateThingAsync(UpdateThingRequest request, CancellationToken cancellationToken);

    Task UpdateThumbnailsAsync(Guid id, List<ThingThumbnail> thumbails, CancellationToken cancellationToken);
}

public class UpdateThingRequest
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Guid TypeId { get; set; }

    public Guid? ClassId { get; set; }

    public Guid? CorrespondentId { get; set; }

    public Guid? ReceiverId { get; set; }

    public IEnumerable<UpdateThingPropertyRequest> Properties { get; set; } = Enumerable.Empty<UpdateThingPropertyRequest>();
}

public class UpdateThingPropertyRequest
{
    public Guid? Id { get; set; }

    public Guid DefinitionId { get; set; }

    public string? Value { get; set; }
}

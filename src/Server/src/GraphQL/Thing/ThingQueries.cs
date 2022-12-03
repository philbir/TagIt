namespace TagIt.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class ThingQueries
{
    [UsePaging]
    [UseFiltering]
    public Task<IQueryable<Thing>> GetThingsAsync(
        [Service] IThingService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }

    public Task<Thing> GetThingByIdAsync(
        [ID(nameof(Thing))] Guid id,
        [Service] IThingService service,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(id, cancellationToken);
    }

    [UseFiltering]
    public Task<IQueryable<ThingType>> GetThingTypesAsync(
        [Service] IThingTypeService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }

    [UseFiltering]
    public Task<IQueryable<ThingClass>> GetThingClassesAsync(
        [Service] IThingClassService service,
        CancellationToken cancellationToken)
    {
        return service.Query(cancellationToken);
    }
}

public static class UpdateThingInputExtensions
{
    internal static UpdateThingRequest ToRequest(this UpdateThingInput input)
    {
        return new UpdateThingRequest
        {
            Id = input.Id,
            TypeId = input.TypeId,
            ClassId = input.ClassId,
            ReceiverId = input.ReceiverId,
            CorrespondentId = input.CorrespondentId,
            Title = input.Title,
            Properties = input.Properties?.Select(p => new UpdateThingPropertyRequest
            {
                Id = p.Id,
                DefinitionId = p.DefinitionId,
                Value = p.Value
            }) ?? Enumerable.Empty<UpdateThingPropertyRequest>()
        };
    }
}

public class UpdateThingInput
{
    [ID(nameof(Thing))]
    public Guid Id { get; set; }

    public string Title { get; set; }

    [ID(nameof(ThingType))]
    public Guid TypeId { get; set; }

    [ID(nameof(ThingClass))]
    public Guid? ClassId { get; set; }

    [ID(nameof(Correspondent))]
    public Guid? CorrespondentId { get; set; }

    [ID(nameof(Receiver))]
    public Guid? ReceiverId { get; set; }

    public IEnumerable<UpdateThingPropertyInput>? Properties { get; set; }
}

public class UpdateThingPropertyInput
{
    [ID(nameof(ThingPropery))]
    public Guid? Id { get; set; }

    [ID(nameof(PropertyDefinition))]
    public Guid DefinitionId { get; set; }

    public string? Value { get; set; }
}

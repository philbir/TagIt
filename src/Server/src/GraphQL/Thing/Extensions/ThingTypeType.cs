namespace TagIt.GraphQL;

public partial class ThingTypeType : ObjectType<ThingType>
{
    protected override void Configure(IObjectTypeDescriptor<ThingType> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.ValidClasses)
            .ResolveWith<Resolvers>(x => x.GetClassesAsync(default!, default!, default));
    }

    class Resolvers
    {
        internal async Task<IEnumerable<ThingClass>> GetClassesAsync(
            [Parent] ThingType thingType,
            [Service] IThingClassService service,
            CancellationToken cancellationToken)
        {
            if (thingType.ValidClasses is { } c && c.Count > 0)
            {
                return await service.GetManyAsync(
                    thingType.ValidClasses,
                    cancellationToken);
            }

            return Array.Empty<ThingClass>();
        }
    }
}



namespace TagIt.GraphQL;

public class ThingPropertyType : ObjectType<ThingPropery>
{
    protected override void Configure(IObjectTypeDescriptor<ThingPropery> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.DefinitionId).ID(nameof(PropertyDefinition));

        descriptor.Field("definition")
            .ResolveWith<Resolvers>(x => x.GetDefinitionAsync(default!, default!, default!));
    }

    class Resolvers
    {
        internal Task<PropertyDefinition> GetDefinitionAsync(
            [DataLoader] PropertyDefinitionByIdDataLoader dataLoader,
            [Parent] ThingPropery property,
            CancellationToken cancellationToken)
                => dataLoader.LoadAsync(property.DefinitionId, cancellationToken)!;
    }
}

using TagIt.Store;

namespace TagIt.GraphQL;

public partial class ThingClassType : ObjectType<ThingClass>
{
    protected override void Configure(IObjectTypeDescriptor<ThingClass> descriptor)
    {
        descriptor.Field(x => x.Properties)
            .ResolveWith<Resolvers>(x => x.GetPropertiesAsync(default!, default!, default));
    }

    class Resolvers
    {
        internal async Task<IEnumerable<PropertyDefinition>> GetPropertiesAsync(
            [Parent] ThingClass thingClass,
            [Service] IPropertyDefinitionStore store,
            CancellationToken cancellationToken)
        {
            if ( thingClass.Properties is { } p && p.Count > 0 )
            {
                return await store.GetManyAsync(thingClass.Properties
                    .Select(x => x.DefinitionId),
                    cancellationToken);
            }

            return Array.Empty<PropertyDefinition>();
        }
    }
}



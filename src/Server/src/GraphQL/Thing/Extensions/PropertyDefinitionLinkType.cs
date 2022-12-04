namespace TagIt.GraphQL;

public partial class PropertyDefinitionLinkType : ObjectType<PropertyDefinitionLink>
{
    protected override void Configure(IObjectTypeDescriptor<PropertyDefinitionLink> descriptor)
    {
        descriptor.Field(x => x.DefinitionId).ID(nameof(PropertyDefinition));
    }
}

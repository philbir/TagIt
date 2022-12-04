namespace TagIt.GraphQL;

public partial class PropertyDefinitionType : ObjectType<PropertyDefinition>
{
    protected override void Configure(IObjectTypeDescriptor<PropertyDefinition> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}

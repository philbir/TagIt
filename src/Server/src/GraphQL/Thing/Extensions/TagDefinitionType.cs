namespace TagIt.GraphQL;

public partial class TagDefinitionType : ObjectType<TagDefinition>
{
    protected override void Configure(IObjectTypeDescriptor<TagDefinition> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}

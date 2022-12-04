namespace TagIt.GraphQL;

public partial class ThingTagType : ObjectType<ThingTag>
{
    protected override void Configure(IObjectTypeDescriptor<ThingTag> descriptor)
    {
        descriptor.Field(x => x.DefintionId).ID(nameof(TagDefinition));
    }
}


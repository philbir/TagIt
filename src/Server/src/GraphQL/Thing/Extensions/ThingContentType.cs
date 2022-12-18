namespace TagIt.GraphQL;

public class ThingContentType : ObjectType<ThingContent>
{
    protected override void Configure(IObjectTypeDescriptor<ThingContent> descriptor)
    {
        descriptor.Field(x => x.ThingId).Ignore();
    }
}

public class TokenDataType : ObjectType<ContentTokenData>
{
    protected override void Configure(IObjectTypeDescriptor<ContentTokenData> descriptor)
    {
        descriptor.Field(x => x.Value)
            .Resolve(c => c.Parent<ContentTokenData>().Value.ToString())
            .Type<StringType>();
    }
}

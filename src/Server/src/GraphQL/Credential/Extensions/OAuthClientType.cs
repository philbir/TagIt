namespace TagIt.GraphQL;

public class OAuthClientType : ObjectType<OAuthClient>
{
    protected override void Configure(IObjectTypeDescriptor<OAuthClient> descriptor)
    {
        descriptor.Field(x => x.Secret)
            .Resolve(x => "***");
    }
}

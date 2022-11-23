namespace TagIt.GraphQL;

public class CredentialTokenType : ObjectType<CredentialToken>
{
    protected override void Configure(IObjectTypeDescriptor<CredentialToken> descriptor)
    {
        descriptor.Field(x => x.Value)
            .Resolve(x => "***");
    }
}

namespace TagIt.GraphQL;

public class CredentialType : ObjectType<Credential>
{
    protected override void Configure(IObjectTypeDescriptor<Credential> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}


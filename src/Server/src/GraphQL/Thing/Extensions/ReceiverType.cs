namespace TagIt.GraphQL;

public partial class ReceiverType : ObjectType<Receiver>
{
    protected override void Configure(IObjectTypeDescriptor<Receiver> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
    }
}


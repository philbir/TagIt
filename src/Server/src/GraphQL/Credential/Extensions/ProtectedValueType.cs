namespace TagIt.GraphQL;

public class ProtectedValueType : ObjectType<ProtectedValue>
{
    protected override void Configure(IObjectTypeDescriptor<ProtectedValue> descriptor)
    {
        descriptor.Ignore(x => x.Value);
    }
}

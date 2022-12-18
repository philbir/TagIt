namespace TagIt.GraphQL;

public class ThingContentNodeType : ObjectType<ThingContentNode>
{
    protected override void Configure(IObjectTypeDescriptor<ThingContentNode> descriptor)
    {
        descriptor.Field(x => x.ThingId).Ignore();

        descriptor.Field("items")
            .ResolveWith<Resolvers>(x => x.GetContentsAsync(default!, default!, default!));

        descriptor.Field("text")
            .ResolveWith<Resolvers>(x => x.GetContentTextAsync(default!, default!, default!));

        descriptor.Field("tokens")
            .ResolveWith<Resolvers>(x => x.GetTokensAsync(default!, default!, default!, default!));

        descriptor.Field("detectedCorrespondents")
            .ResolveWith<Resolvers>(x => x.GetDetectedCorrespondentsAsync(default!, default!, default!, default!));

    }
    class Resolvers
    {
        internal async Task<IReadOnlyList<ThingContent>> GetContentsAsync(
            [Parent] ThingContentNode node,
            [DataLoader] ThingContentDataLoader loader,
            CancellationToken cancellationToken)
        {
            ThingContentAccessor content = await  loader.LoadAsync(node.ThingId, cancellationToken);

            return content.Items.ToList();
        }

        internal async Task<string> GetContentTextAsync(
            [Parent] ThingContentNode node,
            [DataLoader] ThingContentDataLoader loader,
            CancellationToken cancellationToken)
        {
            ThingContentAccessor content = await loader.LoadAsync(
                node.ThingId,
                cancellationToken);

            return content.GetAllText();
        }

        internal async Task<IReadOnlyList<ContentTokenData>> GetTokensAsync(
            [Parent] ThingContentNode node,
            [DataLoader] ThingContentDataLoader loader,
            [Service] IContentTokenizerService service,
            CancellationToken cancellationToken)
        {
            ThingContentAccessor content = await loader.LoadAsync(
                node.ThingId,
                cancellationToken);

            return await service.TokenizeAsync(content.GetAllText(), cancellationToken);
        }

        internal async Task<IReadOnlyList<DetectResult<Correspondent>>> GetDetectedCorrespondentsAsync(
            [Parent] ThingContentNode node,
            [DataLoader] ThingContentDataLoader loader,
            [Service] ICorrespondentService service,
            CancellationToken cancellationToken)
        {
            ThingContentAccessor content = await loader.LoadAsync(
                node.ThingId,
                cancellationToken);

            return await service.DetectFromContentAsync(content, cancellationToken);
        }
    }
}

public class ThingContentNode
{
    public ThingContentNode(Guid thingId)
    {
        ThingId = thingId;
    }

    public Guid ThingId { get; }
}

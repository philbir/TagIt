namespace TagIt.Processing;

public class ThingPostProcessingService : IThingPostProcessingService
{
    private readonly IThingService _thingService;
    private readonly IContentExtractionService _contentExtractionService;
    private readonly IThingContentService _contentService;
    private readonly ICorrespondentService _correspondentService;
    private readonly IThingTypeService _thingTypeService;
    private readonly IThingClassService _thingClassService;
    private readonly ITagDefinitionService _tagDefinitionService;
    private readonly IReceiverService _receiverService;
    private readonly IContentTokenizerService _tokenizerService;

    public ThingPostProcessingService(
        IThingService thingService,
        IContentExtractionService contentExtractionService,
        IThingContentService contentService,
        ICorrespondentService correspondentService,
        IThingTypeService thingTypeService,
        IThingClassService thingClassService,
        ITagDefinitionService tagDefinitionService,
        IReceiverService receiverService,
        IContentTokenizerService tokenizerService)
    {
        _thingService = thingService;
        _contentExtractionService = contentExtractionService;
        _contentService = contentService;
        _correspondentService = correspondentService;
        _thingTypeService = thingTypeService;
        _thingClassService = thingClassService;
        _tagDefinitionService = tagDefinitionService;
        _receiverService = receiverService;
        _tokenizerService = tokenizerService;
    }

    public async Task UpdateContentAsync(Guid thingId, CancellationToken cancellationToken)
    {
        Thing thing = await _thingService.GetByIdAsync(thingId, cancellationToken);

        IReadOnlyList<IThingContentData> contents = await _contentExtractionService.ExtractAsync(
            thing,
            cancellationToken);

        await _contentService.AddContentAsync(thing.Id, contents, cancellationToken);
    }

    public async Task DetectPropertiesAsync(Guid thingId, CancellationToken cancellationToken)
    {
        Thing thing = await _thingService.GetByIdAsync(thingId, cancellationToken);

        ThingContentAccessor? content = await _contentService.GetByThingIdAsync(thingId, cancellationToken);

        if (content is null)
        {
            return;
        }

        IReadOnlyList<ContentTokenData> contentTokens = await _tokenizerService.TokenizeAsync(
            content.GetAllText(),
            cancellationToken);

        await DetectCorrespondentAsync(thing, content, cancellationToken);

        await DetectClassAsync(thing, content, cancellationToken);

        await DetectTagsAsync(thing, content, cancellationToken);

        await DetectReceiverAsync(thing, content, cancellationToken);

        DetectDate(thing, contentTokens);

        await _thingService.UpdateThingAsync(thing, cancellationToken);
    }

    private static void DetectDate(Thing thing, IReadOnlyList<ContentTokenData> contentTokens)
    {
        if (!thing.Date.HasValue)
        {
            IEnumerable<DateTimeOffset> dates = contentTokens
                .Where(x => x.Tokenizer == "Date")
                .Select(x => DateTimeOffset.Parse(x.Value));

            thing.Date = dates
                .Where(x => x < DateTimeOffset.Now)
                .OrderBy(x => x)
                .LastOrDefault();
        }
    }

    private async Task DetectReceiverAsync(Thing thing, ThingContentAccessor content, CancellationToken cancellationToken)
    {
        if (!thing.ReceiverId.HasValue)
        {
            IReadOnlyList<DetectResult<Receiver>> receivers = await _receiverService.DetectFromConteÈntAsync(
                content,
                cancellationToken);

            thing.ReceiverId = receivers.FirstOrDefault()?.Item.Id;
        }
    }

    private async Task DetectTagsAsync(Thing thing, ThingContentAccessor content, CancellationToken cancellationToken)
    {
        if (thing.Tags is null || !thing.Tags.Any())
        {
            IReadOnlyList<DetectResult<TagDefinition>> tags = await _tagDefinitionService.DetectFromContentAsync(
                content,
                cancellationToken);

            thing.Tags = tags.Select(t => new ThingTag() { DefintionId = t.Item.Id }).ToList();
        }
    }

    private async Task DetectClassAsync(Thing thing, ThingContentAccessor content, CancellationToken cancellationToken)
    {
        if (!thing.ClassId.HasValue && thing.TypeId.HasValue)
        {
            IReadOnlyList<DetectResult<ThingClass>> classes = await _thingClassService.DetectFromContentAsync(
                thing.TypeId.Value,
                content,
                cancellationToken);

            thing.ClassId = classes.FirstOrDefault()?.Item.Id;
        }
    }

    private async Task DetectCorrespondentAsync(Thing thing, ThingContentAccessor content,
        CancellationToken cancellationToken)
    {
        if (!thing.CorespondentId.HasValue)
        {
            IReadOnlyList<DetectResult<Correspondent>> correspondents =
                await _correspondentService.DetectFromContentAsync(
                    content,
                    cancellationToken);

            thing.CorespondentId = correspondents.FirstOrDefault()?.Item.Id;
        }
    }
}

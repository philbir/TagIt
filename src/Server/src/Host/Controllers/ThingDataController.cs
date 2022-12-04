using HotChocolate.Types.Relay;
using Microsoft.AspNetCore.Mvc;
using TagIt.Store;

namespace TagIt.Controllers;

[Route("api/thing")]
public class ThingDataController : Controller
{
    private readonly IIdSerializer _idSerializer;
    private readonly IThingDataResolver _thingDataResolver;
    private readonly IThingService _thingService;
    private readonly IThumbnailStore _thumbnailStore;

    public ThingDataController(
        IIdSerializer idSerializer,
        IThingDataResolver thingDataResolver,
        IThingService thingService,
        IThumbnailStore thumbnailStore)
    {
        _idSerializer = idSerializer;
        _thingDataResolver = thingDataResolver;
        _thingService = thingService;
        _thumbnailStore = thumbnailStore;
    }

    [HttpGet]
    [Route("preview/{id}/{type}")]
    public async Task<IActionResult> PreviewAsync(
        string id,
        string type,
        CancellationToken cancellationToken)
    {
        IdValue idType = _idSerializer.Deserialize(id);

        Thing thing = await _thingService.GetByIdAsync(
            (Guid)idType.Value,
            cancellationToken);


        ThingData data = await _thingDataResolver.GetOriginalAsync(
            thing,
            cancellationToken);
        return new FileStreamResult(data.Stream, "application/pdf");
    }

    [HttpGet]
    [Route("thumbnail/{id}/{format}")]
    public async Task<IActionResult> GetThumbnailAsync(
        string id,
        string format,
        CancellationToken cancellationToken)
    {
        var data = await _thumbnailStore.GetAsync(id, cancellationToken);

        return new FileContentResult(data, $"image/{format.ToLower()}");
    }
}

using HotChocolate.Types.Relay;
using Microsoft.AspNetCore.Mvc;

namespace TagIt.Controllers;

[Route("api/thing")]
public class ThingDataController : Controller
{
    private readonly IIdSerializer _idSerializer;
    private readonly IThingDataResolver _thingDataResolver;
    private readonly IThingService _thingService;

    public ThingDataController(
        IIdSerializer idSerializer,
        IThingDataResolver thingDataResolver,
        IThingService thingService)
    {
        _idSerializer = idSerializer;
        _thingDataResolver = thingDataResolver;
        _thingService = thingService;
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
}

using TagIt.Connectors;
using TagIt.Image;
using TagIt.Store;

namespace TagIt;
public class ThumbnailGeneratorService : IThumbnailGeneratorService
{
    private readonly IThingService _thingService;
    private readonly IThingDataService _dataService;
    private readonly IConnectorFactory _connectorFactory;
    private readonly IEnumerable<IImageExtractor> _extractors;
    private readonly IImageConverter _imageConverter;
    private readonly IThumbnailStore _thumbnailStore;

    public ThumbnailGeneratorService(
        IThingService thingService,
        IThingDataService dataService,
        IConnectorFactory connectorFactory,
        IEnumerable<IImageExtractor> extractors,
        IImageConverter imageConverter,
        IThumbnailStore thumbnailStore)
    {
        _thingService = thingService;
        _dataService = dataService;
        _connectorFactory = connectorFactory;
        _extractors = extractors;
        _imageConverter = imageConverter;
        _thumbnailStore = thumbnailStore;
    }

    public async Task UpdateThumbnailsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Thing thing = await _thingService.GetByIdAsync(id, cancellationToken);

        IEnumerable<ImageData> thumbnails = await GenerateThumbmailsAsync(thing, cancellationToken);

        var storedThumbnails = new List<ThingThumbnail>();

        foreach (ImageData thumbnail in thumbnails)
        {
            var thumbnailReference = new ThingThumbnail
            {
                FileId = Guid.NewGuid().ToString("N"),
                Format = thumbnail.Format,
                Size = thumbnail.Size
            };

            await _thumbnailStore.SaveAsync(thumbnailReference.FileId, thumbnail.Data, cancellationToken);

            storedThumbnails.Add(thumbnailReference);
        }

        await _thingService.UpdateThumbnailsAsync(thing.Id, storedThumbnails, cancellationToken);
    }

    public async Task<IEnumerable<ImageData>> GenerateThumbmailsAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        IConnector connector = await _connectorFactory.CreateAsync(
            thing.Source.ConnectorId,
            cancellationToken);

        var thumbnails = new List<ImageData>();

        if (connector.Description.HasThumbnailGenerator)
        {
            // TODO: Use connector thumbnail generator
        }
        else
        {
            ThingData data = await _dataService
                .GetOriginalAsync(thing, cancellationToken);

            IImageExtractor? imageExtractor = _extractors.FirstOrDefault(
                x => x.SupportedTypes.Contains(data.ContentType));

            var convertOptions = new ConvertImageOptions
            {
                Format = ImageFormat.WebP,
                Size = new ImageSize { Width = 300, Height = 400 },
                Quality = 80
            };

            if (imageExtractor is { })
            {
                Stream image = await imageExtractor.ExtractAsync(
                    data.Stream,
                    new ExtractImageOptions(),
                    cancellationToken);

                image.Seek(0, SeekOrigin.Begin);

                ImageData thumbnailData = await _imageConverter.ConvertAsync(
                    image,
                    convertOptions,
                    cancellationToken);

                thumbnails.Add(thumbnailData);
            }
        }

        return thumbnails;
    }
}

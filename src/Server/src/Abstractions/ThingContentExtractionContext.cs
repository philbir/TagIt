namespace TagIt;

public class ThingContentExtractionContext
{
    public ThingContentExtractionContext(Thing thing)
    {
        Thing = thing;
    }

    public Thing Thing { get; }

    public Stream Archived { get; set; }

    public ThingData ArchivedData { get; set; }
}

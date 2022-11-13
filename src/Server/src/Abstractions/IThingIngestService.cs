namespace TagIt;

public interface IThingIngestService
{
    Task AddAsync(AddThingRequest request, CancellationToken cancellationToken);
}
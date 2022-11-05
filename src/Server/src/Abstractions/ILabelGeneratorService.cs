namespace TagIt;
public interface ILabelGeneratorService
{
    Task<string> CreateLabelAsync(
        Thing thing,
        CancellationToken cancellationToken);
}

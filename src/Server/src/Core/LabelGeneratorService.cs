namespace TagIt;

public class LabelGeneratorService : ILabelGeneratorService
{
    public Task<string> CreateLabelAsync(
        Thing thing,
        CancellationToken cancellationToken)
    {
        //TODO: Make something usefull here
        return Task.FromResult(thing.Id.ToString("N"));
    }
}


using TagIt.Store;

namespace TagIt.Processing;

public class PdfOcrStep : IWorkflowStep
{
    private readonly IThingService _thingService;
    private readonly IThingDataService _thingDataService;
    private readonly IPdfOcrService _pdfOcrService;

    public PdfOcrStep(
        IThingService thingService,
        IThingDataService thingDataService,
        IPdfOcrService pdfOcrService)
    {
        _thingService = thingService;
        _thingDataService = thingDataService;
        _pdfOcrService = pdfOcrService;
    }

    public async Task<WorkflowStepResult> ExecuteAsync(WorkflowStep step, WorkflowExecutionContext context)
    {
        Thing thing = await _thingService.GetByIdAsync(
            context.Data<ThingWorkflowData>().Id,
            context.CancellationToken);

        ThingData original = await _thingDataService.GetOriginalAsync(thing, context.CancellationToken);

        CreatePdfResult pdf = await _pdfOcrService.CreatePdfAsync(
            new CreatePdfRequest(original.Id, original.Stream),
            context.CancellationToken);

        await _thingDataService.AddDataAsync(thing,
            new AddThingDataRequest
            {
                Stream = new MemoryStream(pdf.Data),
                Filename = $"{thing.Id:N}_Archive.pdf",
                Type = DataRefNames.PdfArchive
            }, context.CancellationToken);

        return new WorkflowStepResult();
    }

    public string Name => WorkflowStepNames.PdfOcr;
}

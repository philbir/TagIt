namespace TagIt.Processing;

public class OCRStep : IWorkflowStep
{
    private readonly IThingService _thingService;
    private readonly IThingDataService _thingDataService;
    private readonly IPdfOcrService _pdfOcrService;

    public OCRStep(
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

        Task<CreatePdfResult> pdf = _pdfOcrService.CreatePdfAsync(
            new CreatePdfRequest(original.Id, original.Stream),
            context.CancellationToken);







    }

    public string Name => WorkflowStepNames.PdfOcr;
}

using Microsoft.Extensions.DependencyInjection;

namespace TagIt.Processing;

public static class WorkflowTemplates
{
    public static WorkflowBuilder RegisterThingPostProcessing(this WorkflowBuilder builder)
    {
        var template = new WorkflowTemplate
        {
            Name = "ThingPostProcess",
            Steps = new List<string>
            {
                WorkflowStepNames.CreateThmumbnails,
                WorkflowStepNames.PdfOcr,
                WorkflowStepNames.ThingContentExtraction,
                WorkflowStepNames.ThingDetectProperties,
                WorkflowStepNames.ThingPostProcessingCompleted
            }
        };

        builder.Services.AddSingleton(template);

        builder.RegisterStep<CreateThumbnailsStep>();
        builder.RegisterStep<PdfOcrStep>();
        builder.RegisterStep<ThingContentExtractionStep>();
        builder.RegisterStep<ThingDetectPropertiesStep>();
        builder.RegisterStep<ThingPostProcessingCompletedStep>();

        return builder;
    }
}

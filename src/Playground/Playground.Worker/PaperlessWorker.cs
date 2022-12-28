using TagIt.PaperlessNgx;

namespace TagIt;

public class PaperlessWorker : BackgroundService
{
    private readonly IPaperlessDocumentClient _paperlessDocumentClient;

    public PaperlessWorker(IPaperlessDocumentClient paperlessDocumentClient)
    {
        _paperlessDocumentClient = paperlessDocumentClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        FileStream file = File.OpenRead(
            "/Users/p7e/Library/CloudStorage/OneDrive-Personal/_My/Insurance/Compare_2022.xlsx");

        CreatePdfResult data = await _paperlessDocumentClient.CreatePdfAsync(
            new CreatePdfRequest("Compare_2022.xlsx", file),
            stoppingToken);

        //await using FileStream archived = File.Create(Path.Combine($"/Users/p7e/TagIt/pl_arch_2.pdf"));

        await File.WriteAllBytesAsync("/Users/p7e/TagIt/pl_arch_2.pdf", data, stoppingToken);

        /*
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(archived, stoppingToken);

        archived.Close();*/
    }
}

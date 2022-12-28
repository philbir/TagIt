
using System.Net.Http.Json;

namespace TagIt.PaperlessNgx;

public class PaperlessPaperlessDocumentClient : IPaperlessDocumentClient, IPdfOcrService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PaperlessPaperlessDocumentClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> UploadAsync(string name, Stream stream, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreatePaperlessNgxClient();

        using var multipartFormContent = new MultipartFormDataContent();
        var id = Guid.NewGuid().ToString("N");

        var fileStreamContent = new StreamContent(stream);
        multipartFormContent.Add(fileStreamContent, name: "document", fileName: id + Path.GetExtension(name));

        HttpResponseMessage result = await client.PostAsync(
            "/api/documents/post_document/",
            multipartFormContent,
            cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            var t = await result.Content.ReadAsStringAsync();
        }

        return id;
    }

    public async Task<CreatePdfResult> CreatePdfAsync(CreatePdfRequest request, CancellationToken cancellationToken)
    {
        string id = await UploadAsync(request.Filename, request.Stream, cancellationToken);

        Document document = await WaitProcessingCompletedAsync(id, cancellationToken);

        var file = await DownloadAsync(document.Id, cancellationToken);

        await DeleteAsync(document.Id, cancellationToken);

        return new CreatePdfResult(file);
    }

    private async Task<byte[]> DownloadAsync(int id, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreatePaperlessNgxClient();

        var file = await client.GetByteArrayAsync(
            $"/api/documents/{id}/download/",
            cancellationToken);

        return file;
    }

    private async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreatePaperlessNgxClient();

        HttpResponseMessage result = await client.DeleteAsync(
            $"/api/documents/{id}/",
            cancellationToken);

        result.EnsureSuccessStatusCode();
    }

    private async Task<Document> WaitProcessingCompletedAsync(string id, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(4));
        var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(120));

        var completion = new TaskCompletionSource<Document>();
        timeoutToken.Token.Register(() => completion.TrySetCanceled());

        try
        {
            while (!timeoutToken.Token.IsCancellationRequested)
            {
                Document? document = await IsDocumentReadyAsync(id, cancellationToken);

                if (document is { })
                {
                    completion.TrySetResult(document);
                    break;
                }

                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }
        catch (Exception ex)
        {
            completion.TrySetException(ex);
        }
        finally
        {
            timeoutToken.Dispose();
        }

        return completion.Task.Result;
    }

    private async Task<Document?> IsDocumentReadyAsync(string id, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreatePaperlessNgxClient();

        DocumentSearchResult? response = await client.GetFromJsonAsync<DocumentSearchResult>(
            $"/api/documents/?query={id}",
            cancellationToken);

        if (response?.Count == 1)
        {
            return response.Results.First();
        }

        return null;
    }
}




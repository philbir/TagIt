using Microsoft.Graph;

namespace TagIt.MicrosoftGraph;

public static class ResourceDataExtensions
{
    public static string? GetId(this ResourceData resource)
    {
        return resource.AdditionalData["id"]?.ToString();
    }
}

using System.Text;
namespace TagIt;

public class TotalyUselessAndUnsafeValueProtector : IValueProtector
{
    public Task<byte[]> ProtectAsync(
        string value,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(Encoding.UTF8.GetBytes(value));
    }

    public Task<string> UnProtectAsync(
        byte[] cypher,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(Encoding.UTF8.GetString(cypher));
    }
}

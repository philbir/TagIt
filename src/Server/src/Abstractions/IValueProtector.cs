
namespace TagIt;

public interface IValueProtector
{
    Task<byte[]> ProtectAsync(
        string value,
        CancellationToken cancellationToken);

    Task<string> UnProtectAsync(
        byte[] cypher,
        CancellationToken cancellationToken);
}


#nullable disable


namespace TagIt;

public record ProtectedValue
{
    public byte[] Cipher { get; set; }

    public string Value { get; set; }
}

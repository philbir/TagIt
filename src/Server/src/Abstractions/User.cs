#nullable disable

using TagIt.Store;

namespace TagIt;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string Email { get; set; }
}

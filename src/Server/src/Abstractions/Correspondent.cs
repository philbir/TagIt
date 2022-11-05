#nullable disable

using TagIt.Store;

namespace TagIt;

public class Correspondent : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class Receiver : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
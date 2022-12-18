using TagIt.Store;
#nullable disable

namespace TagIt;

public class JobRun : IEntity
{
    public Guid Id { get; set; }

    public Guid JobDefintionId { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public JobRunState State { get; set; }

    public IList<string> Messages { get; set; } = new List<string>();

    public JobDefintion JobDefintion { get; set; }
}

public enum JobRunState
{
    Running,
    Completed,
    Failed
}




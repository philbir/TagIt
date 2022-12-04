using TagIt.Store;
#nullable disable

namespace TagIt;

public class JobDefintion : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public JobRunMode RunMode { get; set; }

    public string? CronSchedule { get; set; }

    public JobSchedule? Schedule { get; set; }

    public string? Filter { get; set; }

    public Guid SourceConnectorId { get; set; }

    public JobAction Action { get; set; }

    public bool Enabled { get; set; }
}


public class JobSchedule
{
    public JobScheduleType Type { get; set; }

    public string? CronExpression { get; set; }

    /// <summary>
    /// Intervall in seconds
    /// </summary>
    public int? Intervall { get; set; }
}

public enum JobScheduleType
{
    Interval,
    Cron
}

public class JobAction
{
    public JobActionMode Mode { get; set; }

    public SourceAction Source { get; set; }

    public Guid? DestinationConnectorId { get; set; }

}

public class SourceAction
{
    public SourceActionMode Mode { get; set; }

    public string? NewLocation { get; set; }

    public Guid? NewConnectorId { get; set; }
}

public enum SourceActionMode
{
    Delete,
    Move
}

public enum JobActionMode
{
    Import,
    Link
}

public enum JobRunMode
{
    Watch,
    Harvest
}

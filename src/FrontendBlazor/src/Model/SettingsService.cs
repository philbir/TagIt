using static Routing;
using StrawberryShake;
using TagIt.UI;
using System.Reactive.Linq;
using System.IO;
using TagIt.UI.Jobs;

public class SettingsService
{
    private readonly ITagItClient _tagItClient;
    private IDisposable connectorStore;
    private IDisposable JobDefintionStore;

    public IEnumerable<ConnectorItem> Connectors { get; set; } = new List<ConnectorItem>();

    public IEnumerable<JobDefinitionItem> JobDefintions { get; set; } = new List<JobDefinitionItem>();

    public SettingsService(ITagItClient tagItClient)
    {
        _tagItClient = tagItClient;
    }

    public void LoadConnectors(Action<IEnumerable<ConnectorItem>> onLoaded)
    {
        if (Connectors.Count() > 0)
        {
            onLoaded(Connectors);
            return;
        }

        connectorStore = _tagItClient.ConnectorSearch
         .Watch(ExecutionStrategy.CacheAndNetwork)
         .Where(x => !x.Errors.Any())
         .Select(x => x.Data.Connectors.Nodes.Select(
             t => new ConnectorItem(t.Id, t.Name, t.Type, t.Root, t.Credential?.Name)))
         .Subscribe(result =>
         {
             Connectors = result;
             onLoaded(result);
         });
    }

    public async Task SaveJobDefinitionAsync(EditJobDefinitionViewModel model)
    {
        var input = new UpdateJobDefinitionInput
        {
            Id = model.Id,
            Enabled = true,
            Name = model.Name,
            Filter = model.Filter,
            SourceConnectorId = model.SourceConnector.Id,
            RunMode = model.RunMode,
            Action = new JobActionInput
            {
                DestinationConnectorId = model.Action.DestinationConnector.Id,
                Mode = model.Action.Mode,
                Source = new SourceActionInput
                {
                    Mode = model.Action.SourceActionMode,
                    NewLocation = model.Action.NewLocation,
                }
            },
            Schedule = new JobScheduleInput
            {
                Type = model.ScheduleType,
                CronExpression = model.CronExpression,
                Intervall = model.Intervall
            }
        };

        IOperationResult<IUpdateJobDefintionResult> result = await _tagItClient
            .UpdateJobDefintion.ExecuteAsync(input);

    }

    public void LoadJobDefintionsAsync(Action<IEnumerable<JobDefinitionItem>> onLoaded)
    {
        if (JobDefintions.Count() > 0)
        {
            onLoaded(JobDefintions);
            return;
        }

        JobDefintionStore = _tagItClient.JobDefintionSearch
         .Watch(ExecutionStrategy.CacheAndNetwork)
         .Where(x => !x.Errors.Any())
         .Select(x => x.Data.JobDefintions.Nodes.Select(
             t => new JobDefinitionItem(
                 t.Id,
                 t.Name,
                 t.RunMode.ToString(),
                 t.Filter,
                 t.SourceConnector.Name,
                 t.Enabled)))

         .Subscribe(result =>
         {
             JobDefintions = result;
             onLoaded(result);
         });
    }
}


public class ConnectorItem
{

    public ConnectorItem(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public ConnectorItem(string id, string name, string type, string root, string? credentialName)
    {
        Id = id;
        Name = name;
        Type = type;
        Root = root;
        CredentialName = credentialName;
    }

    public string Id { get; }
    public string Name { get; }
    public string Type { get; }
    public string Root { get; }
    public string CredentialName { get; }

    public override bool Equals(object o)
    {
        var other = o as ConnectorItem;
        return other?.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}


public record JobDefinitionItem(
    string Id,
    string Name,
    string RunMode,
    string Filter,
    string SourceConnectorName, bool Enabled);

public static class ViewModelServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<SettingsService>();

        return services;
    }
}

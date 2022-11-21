using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace TagIt.UI.Jobs;

public partial class EditJobDefintionPage
{
    [Inject] GetobDefinitionByIdQuery Query { get; set; }

    [Inject] SettingsService Service { get; set; }

    [Parameter] public string Id { get; set; }

    string raw;

    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    bool success;
    string[] errors = Array.Empty<string>();
    MudForm form;

    EditJobDefinitionViewModel model = null;

    protected async override Task OnInitializedAsync()
    {
        IOperationResult<IGetobDefinitionByIdResult> result = await Query.ExecuteAsync(Id);
        IGetobDefinitionById_JobDefinitionById definition = result.Data.JobDefinitionById;

        model = new EditJobDefinitionViewModel();
        Service.LoadConnectors((connectors) =>
        {
            model.AllConnectors = connectors;
            StateHasChanged();
        });

        model.Name = definition.Name;
        model.RunMode = definition.RunMode;
        model.SourceConnector = new ConnectorItem(
            definition.SourceConnector.Id,
            definition.SourceConnector.Name);

        model.Filter = definition.Filter;

        model.Action = new JobActionModel();
        model.Action.Mode = definition.Action.Mode;
        model.Action.DestinationConnector = new ConnectorItem(
            definition.Action.DestinationConnector.Id,
            definition.Action.DestinationConnector.Name);

        model.Action.SourceActionMode = definition.Action.Source.Mode;
        model.Action.NewLocation = definition.Action.Source.NewLocation;

        if (model.RunMode == JobRunMode.Harvest && definition.Schedule is { } schedule)
        {
            model.ScheduleType = schedule.Type;
            model.CronExpression = schedule.CronExpression;
            model.Intervall = schedule.Intervall;
        }
    }

    async Task Save()
    {
        Snackbar.Add($"Job save", Severity.Success);

        raw = JsonSerializer.Serialize(model,
            typeof(EditJobDefinitionViewModel),
            new JsonSerializerOptions { WriteIndented = true });

        raw = model.RunMode.ToString();
        await Service.SaveJobDefinitionAsync(model);

        //NavigationManager.NavigateTo(Routing.JobDefintions.Page);
    }
}

public class EditJobDefinitionViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Filter { get; set; }

    public IEnumerable<JobRunMode> RunModeOptions
        => Enum.GetValues(typeof(JobRunMode)).Cast<JobRunMode>();

    public IEnumerable<JobActionMode> JobActionModeOptions
        => Enum.GetValues(typeof(JobActionMode)).Cast<JobActionMode>();

    public IEnumerable<SourceActionMode> SourceActionModeOptions
        => Enum.GetValues(typeof(SourceActionMode)).Cast<SourceActionMode>();

    public IEnumerable<JobSchudeleType> JobSchudeleTypeOptions
        => Enum.GetValues(typeof(JobSchudeleType)).Cast<JobSchudeleType>();

    public JobRunMode RunMode { get; internal set; }
    public IEnumerable<ConnectorItem> AllConnectors { get; internal set; } = new List<ConnectorItem>();

    public ConnectorItem SourceConnector { get; set; }
    public JobActionModel Action { get; internal set; }
    public JobSchudeleType ScheduleType { get; internal set; }
    public string CronExpression { get; internal set; }
    public int? Intervall { get; internal set; }
}

public class JobActionModel
{
    public JobActionMode Mode { get; internal set; }

    public ConnectorItem DestinationConnector { get; internal set; }

    public SourceActionMode SourceActionMode { get; set; }
    public string NewLocation { get; internal set; }
}

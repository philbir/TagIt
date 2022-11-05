using TagIt.Connectors;
using TagIt.Jobs;
using TagIt.Messaging;

namespace TagIt;

public class WebHookMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHookService _webHookService;
    private readonly IJobManager _jobManager;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IConnectorFactory _connectorFactory;

    public WebHookMiddleware(
        RequestDelegate next,
        IWebHookService webHookService,
        IJobManager jobManager,
        IMessagePublisher messagePublisher,
        IConnectorFactory connectorFactory)
    {
        _next = next;
        _webHookService = webHookService;
        _jobManager = jobManager;
        _messagePublisher = messagePublisher;
        _connectorFactory = connectorFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        RouteValueDictionary routeData = context.GetRouteData().Values;
        var id = Guid.Parse(routeData["id"].ToString());

        WebHook hook = await _webHookService.GetByIdAsync(id, context.RequestAborted);

        if (hook == null)
        {
            context.Response.StatusCode = 404;
            return;
        }

        JobDefintion job = await _jobManager.GetDefintionAsync(hook.JobId, context.RequestAborted);

        if (!job.Enabled)
        {
            //Just ignore
            context.Response.StatusCode = 202;

            return;
        }

        IConnector connector = await _connectorFactory
            .CreateAsync(
                job.SourceConnectorId,
                context.RequestAborted);

        ProcessHookResult result = await connector.ProcessWebHookRequestAsync(context, hook);

        foreach (ConnectorItem item in result.Items)
        {
            await _messagePublisher.PublishAsync(
                new NewConnectorItemMessage(item, job.Action)
                {
                    RequestItemInfo = result.RequestItemInfo
                },
                context.RequestAborted);
        }
    }
}

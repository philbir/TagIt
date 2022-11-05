using HotChocolate;
using Microsoft.AspNetCore.Routing.Patterns;
using static Microsoft.AspNetCore.Routing.Patterns.RoutePatternFactory;

namespace TagIt;

public static class WebHookEndpointRouteBuilder
{
    public static IEndpointRouteBuilder MapWebHooks(
        this IEndpointRouteBuilder endpointRouteBuilder)
    {
        RoutePattern pattern = Parse("hooks/{id:guid}");
        IApplicationBuilder requestPipeline = endpointRouteBuilder
            .CreateApplicationBuilder();

        requestPipeline
            .UseMiddleware<WebHookMiddleware>()
            .Use(_ => context =>
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            });

        endpointRouteBuilder
            .Map(pattern, requestPipeline.Build())
            .WithDisplayName("hooks")
            .AllowAnonymous();

        return endpointRouteBuilder;
    }
}

using TagIt;
using TagIt.ClientAppAuthorization;
using TagIt.Messaging;
using TagIt.Security;
using TagIt.Store.Mongo;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddTagItServer(builder.Configuration)
    .AddGraphQL()
    .AddMicrosoftGraphConnectors()
    .AddMessaging()
    .AddMongoStore();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddSingleton<IUserContextFactory, UserContextFactory>();
WebApplication app = builder.Build();

app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapAuthorizeClient();
    endpoints.MapWebHooks();
    endpoints.MapGraphQL();
});

app.Run();

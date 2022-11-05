using Serilog;
using Serilog.Events;

namespace TagIt;

public static class Logging
{
    public static void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
        .CreateLogger();
    }
}

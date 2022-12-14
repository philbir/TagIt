using Serilog;

namespace TagIt;

public static class Logging
{
    public static void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            //.MinimumLevel.Debug("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
        .CreateLogger();
    }
}


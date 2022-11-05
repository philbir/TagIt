using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.Configuration;

public class TagItServerBuilder : ITagItServerBuilder
{
    public TagItServerBuilder(
        IConfiguration configuration,
        IServiceCollection services)
    {
        Configuration = configuration;
        Services = services;
    }

    public IConfiguration Configuration { get; }
    public IServiceCollection Services { get; }
}

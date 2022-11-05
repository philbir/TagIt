using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TagIt.Configuration;

public interface ITagItServerBuilder
{
    IConfiguration Configuration { get; }
    IServiceCollection Services { get; }
}

using Microsoft.Extensions.DependencyInjection;

namespace Common.Interfaces
{
    public interface ICommonInitializer
    {
        IServiceCollection Initialize(IServiceCollection serviceCollection);
    }
}

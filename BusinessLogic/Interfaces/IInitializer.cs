using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Interfaces
{
    public interface IInitializer
    {
        IServiceCollection Initialize(IServiceCollection serviceCollection);
    }
}

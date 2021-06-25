using Microsoft.Extensions.DependencyInjection;

namespace Database.Interfaces
{
    public interface IDatabaseInitializer
    {
        IServiceCollection Initialize(IServiceCollection serviceCollection);
    }
}

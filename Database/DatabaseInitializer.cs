using Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public IServiceCollection Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DatabaseContext>();

            return serviceCollection;
        }
    }
}

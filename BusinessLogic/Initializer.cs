using BusinessLogic.Interfaces;
using BusinessLogic.Providers;
using BusinessLogic.Providers.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Common;
using Common.Interfaces;
using Database;
using Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public class Initializer : IInitializer
    {
        private ICommonInitializer commonInitializer;

        private IDatabaseInitializer databaseInitializer;

        public IServiceCollection Initialize(IServiceCollection serviceCollection)
        {
            commonInitializer = new CommonInitializer();
            databaseInitializer = new DatabaseInitializer();

            serviceCollection = commonInitializer.Initialize(serviceCollection);
            serviceCollection = databaseInitializer.Initialize(serviceCollection);

            serviceCollection = InitializeProviders(serviceCollection);
            serviceCollection = InitializeServices(serviceCollection);

            return serviceCollection;
        }

        private IServiceCollection InitializeProviders(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDatabaseProvider, DatabaseProvider>();

            return serviceCollection;
        }

        private IServiceCollection InitializeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBreedsService, BreedsService>();

            return serviceCollection;
        }
    }
}

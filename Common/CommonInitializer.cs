using Common.Interfaces;
using Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public class CommonInitializer : ICommonInitializer
    {
        public IServiceCollection Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(configuration => configuration.AddProfile(new MappingProfile()));

            return serviceCollection;
        }
    }
}

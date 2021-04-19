using Microsoft.Extensions.DependencyInjection;

namespace BK9K.Web.Infrastructure.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModule(this IServiceCollection services, IModule module)
        {
            module.Setup(services);
            return services;
        }

        public static IServiceCollection AddModule<T>(this IServiceCollection services) where T : IModule, new()
        { return services.AddModule(new T()); }
    }
}
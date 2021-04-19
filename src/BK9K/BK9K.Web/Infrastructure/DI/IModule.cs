using Microsoft.Extensions.DependencyInjection;

namespace BK9K.Web.Infrastructure.DI
{
    public interface IModule
    {
        void Setup(IServiceCollection services);
    }
}
using SystemsRx.Infrastructure;
using SystemsRx.Infrastructure.Dependencies;
using DryIoc;
using EcsRx.Infrastructure.DryIoc;

namespace BK9K.Web.Applications
{
    public abstract class BlazorEcsRxApplication : SystemsRxApplication
    {
        private DryIocDependencyContainer _dependencyContainer;

        protected BlazorEcsRxApplication(Container container)
        {
            _dependencyContainer = new DryIocDependencyContainer(container);
        }
        
        public override IDependencyContainer Container => _dependencyContainer;
    }
}
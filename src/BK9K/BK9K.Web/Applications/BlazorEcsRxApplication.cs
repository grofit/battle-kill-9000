using DryIoc;
using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.DryIoc;

namespace BK9K.Web.Applications
{
    public abstract class BlazorEcsRxApplication : EcsRxApplication
    {
        private DryIocDependencyContainer _dependencyContainer;

        protected BlazorEcsRxApplication(Container container)
        {
            _dependencyContainer = new DryIocDependencyContainer(container);
        }
        
        public override IDependencyContainer Container => _dependencyContainer;
    }
}
using SystemsRx.Events;
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Web.Debug;

namespace BK9K.Web.Modules
{
    public class DebugModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Unbind<IEventSystem>();
            container.Bind<IEventSystem, DebugEventSystem>();
        }
    }
}
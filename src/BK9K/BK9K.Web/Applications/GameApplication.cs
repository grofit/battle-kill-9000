using SystemsRx.Infrastructure.Extensions;
using SystemsRx.Systems;
using BK9K.Game;
using BK9K.Game.Configuration;
using BK9K.Game.Systems;
using BK9K.Web.Modules;
using DryIoc;

namespace BK9K.Web.Applications
{
    public class GameApplication : BlazorEcsRxApplication
    {
        public World World { get; set; }
        public GameConfiguration GameConfiguration { get; set; }

        public GameApplication(Container container) : base(container)
        {}
        
        protected override void ApplicationStarted()
        {
            
        }

        protected override void BindSystems()
        {
            base.BindSystems();
            this.Container.Bind<ISystem, LevelSetupSystem>();
            this.Container.Bind<ISystem, RoundExecutionSystem>();
            this.Container.Bind<ISystem, LevelEndCheckSystem>();
        }
        
        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            World = Container.Resolve<World>();
            GameConfiguration = Container.Resolve<GameConfiguration>();
        }

        protected override void LoadModules()
        {
            base.LoadModules();
            Container.LoadModule(new GameModule());
            Container.LoadModule(new OpenRpgModule());
        }
    }
}
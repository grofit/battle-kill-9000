﻿using BK9K.Game;
using BK9K.Game.Events;
using BK9K.Game.Systems;
using BK9K.Web.Modules;
using DryIoc;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Scheduling;
using EcsRx.Systems;

namespace BK9K.Web.Applications
{
    public class GameApplication : BlazorEcsRxApplication
    {
        public World World { get; set; }

        public GameApplication(Container container) : base(container)
        {}

        protected override void ApplicationStarted()
        {
            EventSystem.Publish(new LevelLoadingEvent(1));
        }

        protected override void BindSystems()
        {
            base.BindSystems();
            this.Container.Bind<ISystem, GridSetupSystem>();
            this.Container.Bind<ISystem, UnitSetupSystem>();
            this.Container.Bind<ISystem, RoundExecutionSystem>();
            this.Container.Bind<ISystem, LevelEndCheckSystem>();
        }
        
        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            World = Container.Resolve<World>();
        }

        protected override void LoadModules()
        {
            base.LoadModules();
            Container.LoadModule(new GameModule());
            Container.LoadModule(new OpenRpgModule());
        }
    }
}
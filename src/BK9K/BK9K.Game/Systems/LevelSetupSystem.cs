﻿using System.Collections.Generic;
using System.Linq;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Grids;
using BK9K.Framework.Types;
using BK9K.Framework.Units;
using BK9K.Game.Builders;
using BK9K.Game.Events;

namespace BK9K.Game.Systems
{
    public class LevelSetupSystem : IReactToEventSystem<RequestLevelLoadEvent>
    {
        public World World { get; }
        public IEventSystem EventSystem { get; }
        public UnitBuilder UnitBuilder { get; }
        
        public LevelSetupSystem(UnitBuilder unitBuilder, World world, IEventSystem eventSystem)
        {
            UnitBuilder = unitBuilder;
            World = world;
            EventSystem = eventSystem;
        }

        public void Process(RequestLevelLoadEvent eventData)
        {
            World.Units = SetupUnits().ToList();
            World.Grid = SetupGrid();
            EventSystem.Publish(new LevelLoadedEvent());
        }

        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }

        private IEnumerable<Unit> SetupUnits()
        {
            yield return UnitBuilder.Create()
                .WithName("Gooch")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Fighter)
                .WithInitiative(6)
                .WithPosition(3, 2)
                .Build();

            yield return UnitBuilder.Create()
                .WithName("Kate")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Fighter)
                .WithInitiative(6)
                .WithPosition(1, 1)
                .Build();

            yield return UnitBuilder.Create()
                .WithName("Enemy Person 1")
                .WithInitiative(3)
                .WithFaction(FactionTypes.Enemy)
                .WithClass(ClassTypes.Mage)
                .WithPosition(3, 3)
                .Build();

            yield return UnitBuilder.Create()
                .WithName("Enemy Person 2")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithClass(ClassTypes.Rogue)
                .WithPosition(3, 1)
                .Build();

            yield return UnitBuilder.Create()
                .WithName("Enemy Person 3")
                .WithFaction(FactionTypes.Enemy)
                .WithInitiative(2)
                .WithClass(ClassTypes.Fighter)
                .WithPosition(2, 2)
                .Build();
        }
    }
}
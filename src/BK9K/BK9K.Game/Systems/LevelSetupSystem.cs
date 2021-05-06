using System;
using System.Collections.Generic;
using System.Linq;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Grids;
using BK9K.Framework.Transforms;
using BK9K.Framework.Units;
using BK9K.Game.Builders;
using BK9K.Game.Configuration;
using BK9K.Game.Events;
using BK9K.Game.Types;
using OpenRpg.Core.Utils;

namespace BK9K.Game.Systems
{
    public class LevelSetupSystem : IReactToEventSystem<RequestLevelLoadEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }
        public UnitBuilder UnitBuilder { get; }
        public IRandomizer Randomizer { get; }
        public GameState GameState { get; }
        
        public LevelSetupSystem(UnitBuilder unitBuilder, Level level, IEventSystem eventSystem, GameState gameState, IRandomizer randomizer)
        {
            UnitBuilder = unitBuilder;
            Level = level;
            EventSystem = eventSystem;
            Randomizer = randomizer;
            GameState = gameState;
        }

        public void Process(RequestLevelLoadEvent eventData)
        {
            Level.Grid = SetupGrid();
            Level.Units = GameState.PlayerUnits.ToList();

            foreach (var enemy in SetupEnemies(eventData.LevelId))
            { Level.Units.Add(enemy); }

            Level.HasLevelFinished = false;
            EventSystem.Publish(new LevelLoadedEvent());
        }

        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
        }
        
        private IEnumerable<Position> GeneratePosition()
        {
            while (true)
            {
                var x = Randomizer.Random(0, Level.Grid.XSize-1);
                var y = Randomizer.Random(0, Level.Grid.YSize-1);
                yield return new Position(x, y);
            }
        }

        private Position FindOpenPosition()
        { return GeneratePosition().First(x => Level.GetUnitAt(x) == null); }

        private IEnumerable<Unit> SetupEnemies(int levelId)
        {
            var minEnemies = levelId / 2;
            if(minEnemies == 0) { minEnemies = 1; }
            var maxEnemies = levelId;

            var actualEnemies = Randomizer.Random(minEnemies, maxEnemies);
            var enemyIndex = 1;

            for (var i = 0; i < actualEnemies; i++)
            {
                var randomInitiative = Randomizer.Random(1, 6);
                var randomClass = Randomizer.Random(ClassTypes.Fighter, ClassTypes.Rogue-1);
                var randomRace = Randomizer.Random(RaceTypes.Human, RaceTypes.Dwarf-1);
                var randomPosition = FindOpenPosition();

                yield return UnitBuilder.Create()
                    .WithName($"Enemy Person {enemyIndex++}")
                    .WithInitiative(randomInitiative)
                    .WithFaction(FactionTypes.Enemy)
                    .WithRace(randomRace)
                    .WithClass(randomClass)
                    .WithPosition(randomPosition)
                    .Build();
            }
        }
    }
}
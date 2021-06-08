using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.AI;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Events.Level;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Pools;
using BK9K.Game.Units;
using BK9K.Mechanics.Loot;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Utils;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;

namespace BK9K.Game.Systems.Levels
{
    public class LevelUnitSetupSystem : IReactToEventSystem<LevelGridSetupCompleteEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }
        public UnitBuilder UnitBuilder { get; }
        public AgentBuilder AgentBuilder { get; }
        public IRandomizer Randomizer { get; }
        public GameState GameState { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        public IUnitIdPool UnitIdPool { get; }
        
        public LevelUnitSetupSystem(UnitBuilder unitBuilder, Level level, IEventSystem eventSystem, GameState gameState, IRandomizer randomizer, IItemTemplateRepository itemTemplateRepository, AgentBuilder agentBuilder, IUnitIdPool unitIdPool)
        {
            UnitBuilder = unitBuilder;
            Level = level;
            EventSystem = eventSystem;
            Randomizer = randomizer;
            ItemTemplateRepository = itemTemplateRepository;
            AgentBuilder = agentBuilder;
            UnitIdPool = unitIdPool;
            GameState = gameState;
        }

        public void Process(LevelGridSetupCompleteEvent eventData)
        {
            DisposeExistingData();
            
            Level.GameUnits.Clear();
            var unitList = new List<Unit>();
            unitList = GameState.PlayerUnits.ToList();

            foreach (var enemy in SetupEnemies(Level.Id))
            { unitList.Add(enemy); }

            SetupUnitPositions(unitList);
            
            foreach (var unit in unitList)
            {
                Console.WriteLine($"{unit.NameLocaleId} is at {unit.Position.X},{unit.Position.Y}");
            }
            
            Level.GameUnits = SetupGameUnits(unitList).ToList();
            EventSystem.Publish(new LevelUnitsSetupCompleteEvent());
        }

        public void DisposeExistingData()
        {
            if (Level.GameUnits.Count != 0)
            { Level.GameUnits.ForEach(x =>
            {
                x.Agent.Dispose();
                UnitIdPool.ReleaseInstance(x.Unit.Id);
            }); }
        }

        public IEnumerable<GameUnit> SetupGameUnits(List<Unit> units)
        {
            foreach (var unit in units)
            {
                var agent = AgentBuilder.Create()
                    .ForUnit(unit)
                    .Build();
                
                yield return new GameUnit(unit, agent);
            }
        }

        private IEnumerable<Vector2> GeneratePosition()
        {
            while (true)
            {
                var x = Randomizer.Random(0, Level.Grid.XSize-1);
                var y = Randomizer.Random(0, Level.Grid.YSize-1);
                yield return new Vector2(x, y);
            }
        }

        private Vector2 FindOpenPosition(IReadOnlyCollection<Unit> nonLevelUnits)
        { return GeneratePosition().First(position => Level.GetUnitAt(position) == null && nonLevelUnits.All(y => y.Position != position)); }

        private void SetupUnitPositions(IReadOnlyCollection<Unit> units)
        {
            foreach (var unit in units)
            {
                var randomPosition = FindOpenPosition(units);
                unit.Position = randomPosition;
            }
        }
        
        private ILootTable GenerateLootTable()
        {
            var potionItemTemplate = ItemTemplateRepository.Retrieve(ItemTemplateLookups.MinorHealthPotion);
            var potionItem = new DefaultItem
            {
                ItemTemplate = potionItemTemplate,
                Modifications = new IModification[0],
                Variables = new DefaultItemVariables()
            };

            var potionLootEntry = new LootTableEntry
            {
                Item = potionItem,
                Requirements = new Requirement[0],
                Variables = new DefaultLootTableEntryVariables()
            };
            potionLootEntry.Variables.DropRate(100);
            potionLootEntry.Variables.IsUnique(false);

            var lootEntries = new List<ILootTableEntry> { potionLootEntry };
            return new DefaultLootTable
            {
                AvailableLoot = lootEntries,
                Randomizer = Randomizer,
            };
        }

        private IEnumerable<Unit> SetupEnemies(int levelId)
        {
            var minEnemies = levelId / 2;
            if(minEnemies == 0) { minEnemies = 1; }
            var maxEnemies = levelId;

            var enemies = new List<EnemyUnit>();
            var actualEnemies = Randomizer.Random(minEnemies, maxEnemies);
            for (var i = 0; i < actualEnemies; i++)
            {
                var randomInitiative = Randomizer.Random(1, 6);
                var randomClass = Randomizer.Random(ClassLookups.Fighter, ClassLookups.Rogue-1);
                var randomRace = Randomizer.Random(RaceLookups.Human, RaceLookups.Dwarf-1);
                var loot = GenerateLootTable();

                var enemyId = UnitIdPool.AllocateInstance();
                var enemyUnit = UnitBuilder.Create()
                    .WithId(enemyId)
                    .WithName($"Enemy Person {enemyId}")
                    .WithInitiative(randomInitiative)
                    .WithFaction(FactionTypes.Enemy)
                    .WithRace(randomRace)
                    .WithClass(randomClass)
                    .Build() as EnemyUnit;

                enemyUnit.LootTable = loot;

                enemies.Add(enemyUnit);
            }

            return enemies;
        }
    }
}
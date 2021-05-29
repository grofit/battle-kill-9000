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
using BK9K.Mechanics.Grids;
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
    public class LevelSetupSystem : IReactToEventSystem<RequestLevelLoadEvent>
    {
        public Level Level { get; }
        public IEventSystem EventSystem { get; }
        public UnitBuilder UnitBuilder { get; }
        public AgentBuilder AgentBuilder { get; }
        public IRandomizer Randomizer { get; }
        public GameState GameState { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        public IUnitIdPool UnitIdPool { get; }
        
        public LevelSetupSystem(UnitBuilder unitBuilder, Level level, IEventSystem eventSystem, GameState gameState, IRandomizer randomizer, IItemTemplateRepository itemTemplateRepository, AgentBuilder agentBuilder, IUnitIdPool unitIdPool)
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

        public void Process(RequestLevelLoadEvent eventData)
        {
            DisposeExistingData();
            
            Level.Grid = SetupGrid();
            var unitList = new List<Unit>();
            unitList = GameState.PlayerUnits.ToList();

            foreach (var enemy in SetupEnemies(eventData.LevelId))
            { unitList.Add(enemy); }

            Level.GameUnits = SetupAI(unitList).ToList();
            Level.HasLevelFinished = false;
            EventSystem.Publish(new LevelLoadedEvent());
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

        public IEnumerable<GameUnit> SetupAI(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                var agent = AgentBuilder.Create()
                    .ForUnit(unit)
                    .Build();
                yield return new GameUnit(unit, agent);
            }
        }

        public Grid SetupGrid()
        {
            return GridBuilder.Create()
                .WithSize(5, 5)
                .Build();
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

        private Vector2 FindOpenPosition()
        { return GeneratePosition().First(x => Level.GetUnitAt(x) == null); }

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

            var actualEnemies = Randomizer.Random(minEnemies, maxEnemies);
            var enemyIndex = 1;

            for (var i = 0; i < actualEnemies; i++)
            {
                var randomInitiative = Randomizer.Random(1, 6);
                var randomClass = Randomizer.Random(ClassLookups.Fighter, ClassLookups.Rogue-1);
                var randomRace = Randomizer.Random(RaceLookups.Human, RaceLookups.Dwarf-1);
                var randomPosition = FindOpenPosition();
                var loot = GenerateLootTable();

                var enemyUnit = UnitBuilder.Create()
                    .WithId(UnitIdPool.AllocateInstance())
                    .WithName($"Enemy Person {enemyIndex++}")
                    .WithInitiative(randomInitiative)
                    .WithFaction(FactionTypes.Enemy)
                    .WithRace(randomRace)
                    .WithClass(randomClass)
                    .WithPosition(randomPosition)
                    .Build() as EnemyUnit;

                enemyUnit.LootTable = loot;

                yield return enemyUnit;
            }
        }
    }
}
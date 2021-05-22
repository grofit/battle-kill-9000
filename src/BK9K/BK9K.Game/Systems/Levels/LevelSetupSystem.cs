using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Configuration;
using BK9K.Game.Data;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Events.Level;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Grids;
using BK9K.Mechanics.Levels;
using BK9K.Mechanics.Loot;
using BK9K.Mechanics.Types;
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
        public IRandomizer Randomizer { get; }
        public GameState GameState { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        
        public LevelSetupSystem(UnitBuilder unitBuilder, Level level, IEventSystem eventSystem, GameState gameState, IRandomizer randomizer, IItemTemplateRepository itemTemplateRepository)
        {
            UnitBuilder = unitBuilder;
            Level = level;
            EventSystem = eventSystem;
            Randomizer = randomizer;
            ItemTemplateRepository = itemTemplateRepository;
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
                var randomClass = Randomizer.Random(ClassTypes.Fighter, ClassTypes.Rogue-1);
                var randomRace = Randomizer.Random(RaceTypes.Human, RaceTypes.Dwarf-1);
                var randomPosition = FindOpenPosition();
                var loot = GenerateLootTable();

                var enemyUnit = UnitBuilder.Create()
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
using System.Collections.Generic;
using System.Threading.Tasks;
using BK9K.Game.AI;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Extensions;
using BK9K.Game.Pools;
using BK9K.Game.Processors;
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

namespace BK9K.Game.Levels.Processors
{
    public class LevelEnemyUnitSetupProcessor : IProcessor<Level>
    {
        public int Priority => 4;
        
        public AgentFactory AgentFactory { get; }
        public IItemTemplateRepository ItemTemplateRepository { get; }
        public IRandomizer Randomizer { get; }
        public IUnitIdPool UnitIdPool { get; }
        public UnitBuilder UnitBuilder { get; }

        public LevelEnemyUnitSetupProcessor(AgentFactory agentFactory, IItemTemplateRepository itemTemplateRepository, IRandomizer randomizer, IUnitIdPool unitIdPool, UnitBuilder unitBuilder)
        {
            AgentFactory = agentFactory;
            ItemTemplateRepository = itemTemplateRepository;
            Randomizer = randomizer;
            UnitIdPool = unitIdPool;
            UnitBuilder = unitBuilder;
        }

        public Task Process(Level context)
        {
            var enemyUnits = SetupEnemies(context.Id);
            var playerGameUnits = AgentFactory.GenerateGameUnits(enemyUnits);
            context.GameUnits.AddRange(playerGameUnits);
            return Task.CompletedTask;
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
    }
}
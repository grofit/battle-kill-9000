using System.Collections.Generic;
using System.Threading.Tasks;
using BK9K.Game.AI;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Extensions;
using BK9K.Game.Pools;
using BK9K.Game.Processors;
using BK9K.Mechanics.Extensions;
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
        public IAbilityRepository AbilityRepository { get; }
        public ISpellRepository SpellRepository { get; }
        public ICardEffectsRepository CardEffectsRepository { get; }
        public IRandomizer Randomizer { get; }
        public IUnitIdPool UnitIdPool { get; }
        public UnitBuilder UnitBuilder { get; }
        
        private ILootTable GlobalLootTable { get; }

        public LevelEnemyUnitSetupProcessor(AgentFactory agentFactory, IItemTemplateRepository itemTemplateRepository, IRandomizer randomizer, IUnitIdPool unitIdPool, UnitBuilder unitBuilder, IAbilityRepository abilityRepository, ISpellRepository spellRepository, ICardEffectsRepository cardEffectsRepository)
        {
            AgentFactory = agentFactory;
            ItemTemplateRepository = itemTemplateRepository;
            Randomizer = randomizer;
            UnitIdPool = unitIdPool;
            UnitBuilder = unitBuilder;
            AbilityRepository = abilityRepository;
            SpellRepository = spellRepository;
            CardEffectsRepository = cardEffectsRepository;

            GlobalLootTable =  GenerateLootTable();
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
                var loot = GlobalLootTable;

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
                enemyUnit.Stats.SetExperience(50);

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

            var cleaveAbility = AbilityRepository.Retrieve(AbilityLookups.Cleave);
            var cleaveLootEntry = new CustomLootTableEntry {Ability = cleaveAbility};
            cleaveLootEntry.Variables.DropRate(0.02f);

            var attackAbility = AbilityRepository.Retrieve(AbilityLookups.Attack);
            var attackLootEntry = new CustomLootTableEntry {Ability = attackAbility};
            attackLootEntry.Variables.DropRate(0.04f);
            
            var healAbility = AbilityRepository.Retrieve(AbilityLookups.Heal);
            var healLootEntry = new CustomLootTableEntry {Ability = healAbility};
            healLootEntry.Variables.DropRate(0.02f);

            var fireboltSpell = SpellRepository.Retrieve(SpellLookups.Firebolt);
            var fireboltLootEntry = new CustomLootTableEntry {Spell = fireboltSpell};
            fireboltLootEntry.Variables.DropRate(0.03f);
            
            var regenSpell = SpellRepository.Retrieve(SpellLookups.Firebolt);
            var regenLootEntry = new CustomLootTableEntry {Spell = regenSpell};
            regenLootEntry.Variables.DropRate(0.03f);

            var minorStrength = CardEffectsRepository.Retrieve(CardEffectLookups.MinorStrength);
            var minorStrengthLootEntry = new CustomLootTableEntry {CardEffects = minorStrength};
            minorStrengthLootEntry.Variables.DropRate(0.05f);
            
            var minorInt = CardEffectsRepository.Retrieve(CardEffectLookups.MinorIntelligence);
            var minorIntLootEntry = new CustomLootTableEntry {CardEffects = minorInt};
            minorIntLootEntry.Variables.DropRate(0.05f);

            var lootEntries = new List<ILootTableEntry>
            {
                potionItem.GenerateCustomLootTableEntry(0.10f),
                cleaveLootEntry,
                attackLootEntry,
                healLootEntry,
                fireboltLootEntry,
                regenLootEntry,
                minorStrengthLootEntry,
                minorIntLootEntry
            };
            return new DefaultLootTable
            {
                AvailableLoot = lootEntries,
                Randomizer = Randomizer,
            };
        }
    }
}
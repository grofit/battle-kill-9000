using System.Collections.Generic;
using SystemsRx.Infrastructure.Extensions;
using SystemsRx.Systems;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Data.Repositories.Defaults;
using BK9K.Game.Levels;
using BK9K.Game.Pools;
using BK9K.Game.Systems.AI;
using BK9K.Game.Systems.Cards;
using BK9K.Game.Systems.Combat;
using BK9K.Game.Systems.Effects;
using BK9K.Game.Systems.EventTranslation;
using BK9K.Game.Systems.Levels;
using BK9K.Mechanics.Cards;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using BK9K.Mechanics.Units;
using BK9K.Web.Modules;
using DryIoc;
using OpenRpg.Cards;
using OpenRpg.Cards.Genres;
using OpenRpg.Core.Modifications;
using OpenRpg.Items;

namespace BK9K.Web.Applications
{
    public class GameApplication : BlazorEcsRxApplication
    {
        public Level Level { get; set; }
        public GameConfiguration GameConfiguration { get; set; }
        public GameState GameState { get; set; }
        public UnitBuilder UnitBuilder { get; set; }
        public IUnitIdPool UnitIdPool { get; set; }
        public IItemTemplateRepository ItemTemplateRepository { get; set; }
        public ICardEffectsRepository CardEffectsRepository { get; set; }
        public ISpellRepository SpellRepository { get; set; }
        public IAbilityRepository AbilityRepository { get; set; }

        public GameApplication(Container container) : base(container)
        {}
        
        protected override void ApplicationStarted()
        {
            var playerTeam = SetupPlayerTeam();
            var playerCards = SetupPlayerCards();
            GameState.PlayerUnits.AddRange(playerTeam);
            GameState.PlayerCards.AddRange(playerCards);
        }

        private IEnumerable<Unit> SetupPlayerTeam()
        {
            yield return UnitBuilder.Create()
                .WithId(UnitIdPool.AllocateInstance())
                .WithName("Gooch")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassLookups.Fighter)
                .WithInitiative(6)
                .WithPosition(3, 2)
                .Build();

            yield return UnitBuilder.Create()
                .WithId(UnitIdPool.AllocateInstance())
                .WithName("Kate")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassLookups.Priest)
                .WithInitiative(6)
                .WithPosition(1, 1)
                .Build();

            yield return UnitBuilder.Create()
                .WithId(UnitIdPool.AllocateInstance())
                .WithName("Le Grandé Tudge")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassLookups.Rogue)
                .WithInitiative(6)
                .WithWeapon(ItemTemplateLookups.DeadlySosig)
                .WithPosition(1, 4)
                .Build();
        }

        private IEnumerable<ICard> SetupPlayerCards()
        {
            var potionItemTemplate = ItemTemplateRepository.Retrieve(ItemTemplateLookups.MinorHealthPotion);
            yield return new ItemCard(new DefaultItem
            {
                ItemTemplate = potionItemTemplate,
                Modifications = new IModification[0],
                Variables = new DefaultItemVariables()
            });

            yield return new EffectCard(CardEffectsRepository.Retrieve(CardEffectLookups.MinorStrength));

            var fireboltSpell = SpellRepository.Retrieve(SpellLookups.Firebolt);
            yield return new SpellCard(fireboltSpell);

            var regenSpell = SpellRepository.Retrieve(SpellLookups.MinorRegen);
            yield return new SpellCard(regenSpell);

            var cleaveAbility = AbilityRepository.Retrieve(AbilityLookups.Cleave);
            yield return new AbilityCard(cleaveAbility);
        }

        protected override void BindSystems()
        {
            base.BindSystems();
            Container.Bind<ISystem, LevelLoadingSystem>();
            Container.Bind<ISystem, UnitDeathTranslationSystem>();
            Container.Bind<ISystem, RoundExecutionSystem>();
            Container.Bind<ISystem, LevelEndCheckSystem>();
            Container.Bind<ISystem, ApplyCardToUnitSystem>();
            Container.Bind<ISystem, EnemyLootingSystem>();
            Container.Bind<ISystem, ApplyCardToTileSystem>();
            Container.Bind<ISystem, EffectTimingSystem>();
            Container.Bind<ISystem, ActionTickedEffectSystem>();
            Container.Bind<ISystem, AgentConsiderationUpdateSystem>();
            Container.Bind<ISystem, ExperienceAllocationSystem>();
            Container.Bind<ISystem, UnitLeveledUpSystem>();
        }
        
        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            Level = Container.Resolve<Level>();
            GameConfiguration = Container.Resolve<GameConfiguration>();
            GameState = Container.Resolve<GameState>();
            UnitBuilder = Container.Resolve<UnitBuilder>();
            ItemTemplateRepository = Container.Resolve<IItemTemplateRepository>();
            CardEffectsRepository = Container.Resolve<ICardEffectsRepository>();
            SpellRepository = Container.Resolve<ISpellRepository>();
            UnitIdPool = Container.Resolve<IUnitIdPool>();
            AbilityRepository = Container.Resolve<IAbilityRepository>();
        }

        protected override void LoadModules()
        {
            base.LoadModules();
            
            Container.LoadModule(new DebugModule());
            Container.LoadModule(new OpenRpgModule());
            Container.LoadModule(new GameModule());
            Container.LoadModule(new GameAIModule());
            Container.LoadModule(new GameDataModule());
        }
    }
}
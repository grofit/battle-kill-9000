using System.Collections.Generic;
using SystemsRx.Infrastructure.Extensions;
using SystemsRx.Systems;
using BK9K.Game.Configuration;
using BK9K.Game.Data;
using BK9K.Game.Data.Builders;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Systems.Cards;
using BK9K.Game.Systems.Combat;
using BK9K.Game.Systems.Effects;
using BK9K.Game.Systems.Levels;
using BK9K.Mechanics.Cards;
using BK9K.Mechanics.Levels;
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
        public IItemTemplateRepository ItemTemplateRepository { get; set; }
        public ICardEffectsRepository CardEffectsRepository { get; set; }
        public ISpellRepository SpellRepository { get; set; }

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
                .WithName("Gooch")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassLookups.Fighter)
                .WithInitiative(6)
                .WithPosition(3, 2)
                .Build();

            yield return UnitBuilder.Create()
                .WithName("Kate")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassLookups.Fighter)
                .WithInitiative(6)
                .WithPosition(1, 1)
                .Build();

            yield return UnitBuilder.Create()
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
        }

        protected override void BindSystems()
        {
            base.BindSystems();
            this.Container.Bind<ISystem, LevelSetupSystem>();
            this.Container.Bind<ISystem, RoundExecutionSystem>();
            this.Container.Bind<ISystem, LevelEndCheckSystem>();
            this.Container.Bind<ISystem, ApplyCardToUnitSystem>();
            this.Container.Bind<ISystem, EnemyLootingSystem>();
            this.Container.Bind<ISystem, ApplyCardToTileSystem>();
            this.Container.Bind<ISystem, EffectTimingSystem>();
            this.Container.Bind<ISystem, ActionTickedEffectSystem>();
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
        }

        protected override void LoadModules()
        {
            base.LoadModules();
            Container.LoadModule(new OpenRpgModule());
            Container.LoadModule(new GameModule());
            Container.LoadModule(new GameDataModule());
        }
    }
}
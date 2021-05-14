using System.Collections.Generic;
using SystemsRx.Infrastructure.Extensions;
using SystemsRx.Systems;
using BK9K.Framework.Cards;
using BK9K.Framework.Levels;
using BK9K.Framework.Units;
using BK9K.Game;
using BK9K.Game.Builders;
using BK9K.Game.Cards;
using BK9K.Game.Configuration;
using BK9K.Game.Data;
using BK9K.Game.Systems;
using BK9K.Game.Types;
using BK9K.Web.Modules;
using DryIoc;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Types;
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
                .WithName("Le Grandé Tudge")
                .WithFaction(FactionTypes.Player)
                .WithClass(ClassTypes.Rogue)
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

            var effects = new List<Effect>
            {
                new() {EffectType = EffectTypes.StrengthBonusAmount, Potency = 1, Requirements = new Requirement[0]}
            };
            yield return new EffectCard("Invigorating Strength", "The card imbues the unit with strength", effects);
        }

        protected override void BindSystems()
        {
            base.BindSystems();
            this.Container.Bind<ISystem, LevelSetupSystem>();
            this.Container.Bind<ISystem, RoundExecutionSystem>();
            this.Container.Bind<ISystem, LevelEndCheckSystem>();
            this.Container.Bind<ISystem, ApplyCardToUnitSystem>();
        }
        
        protected override void ResolveApplicationDependencies()
        {
            base.ResolveApplicationDependencies();
            Level = Container.Resolve<Level>();
            GameConfiguration = Container.Resolve<GameConfiguration>();
            GameState = Container.Resolve<GameState>();
            UnitBuilder = Container.Resolve<UnitBuilder>();
            ItemTemplateRepository = Container.Resolve<IItemTemplateRepository>();
        }

        protected override void LoadModules()
        {
            base.LoadModules();
            Container.LoadModule(new GameModule());
            Container.LoadModule(new OpenRpgModule());
        }
    }
}
using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Builders;
using BK9K.Game.Handlers;
using BK9K.Game.Handlers.Phases;
using BK9K.Game.Handlers.SpellAbilities;
using BK9K.Game.Handlers.UnitAbilities;
using BK9K.Game.Levels;
using BK9K.Mechanics.Grids;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Handlers.Phases;

namespace BK9K.Web.Modules
{
    public class GameModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<GridBuilder>();
            container.Bind<UnitBuilder>();
            container.Bind<Level>();
            container.Bind<GameConfiguration>();
            container.Bind<GameState>();

            container.Bind<IUnitTurnHandler, UnitTurnHandler>();
            container.Bind<IUnitMovementPhaseHandler, UnitMovementPhaseHandler>();
            container.Bind<IUnitActionPhaseHandler, UnitActionPhaseHandler>();
            container.Bind<IAbilityHandler, AttackAbilityHandler>();
            container.Bind<ISpellHandler, FireboltSpellHandler>();
            container.Bind<ISpellHandler, MinorRegenSpellHandler>();
        }
    }
}
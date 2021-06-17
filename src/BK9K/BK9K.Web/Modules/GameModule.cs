using SystemsRx.Infrastructure.Dependencies;
using SystemsRx.Infrastructure.Extensions;
using BK9K.Game.AI;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Builders;
using BK9K.Game.Handlers;
using BK9K.Game.Handlers.Phases;
using BK9K.Game.Handlers.SpellAbilities;
using BK9K.Game.Handlers.UnitAbilities;
using BK9K.Game.Levels;
using BK9K.Game.Levels.Processors;
using BK9K.Game.Movement;
using BK9K.Game.Pools;
using BK9K.Game.Processors;
using BK9K.Mechanics.Grids;
using BK9K.Mechanics.Handlers;

namespace BK9K.Web.Modules
{
    public class GameModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IUnitIdPool, UnitIdPool>();
            
            container.Bind<GridBuilder>();
            container.Bind<UnitBuilder>();
            container.Bind<AgentFactory>();
            container.Bind<Level>();
            container.Bind<GameConfiguration>();
            container.Bind<GameState>();
            
            container.Bind<MovementAdvisor>();

            container.Bind<IProcessor<Level>, CleanLevelProcessor>();
            container.Bind<IProcessor<Level>, LevelGridProcessor>();
            container.Bind<IProcessor<Level>, LevelPlayerUnitSetupProcessor>();
            container.Bind<IProcessor<Level>, LevelEnemyUnitSetupProcessor>();
            container.Bind<IProcessor<Level>, LevelUnitPlacementProcessor>();
            container.Bind<IProcessor<Level>, LevelAgentSetupProcessor>();
            container.Bind<IProcessorRegistry<Level>, DefaultProcessorRegistry<Level>>();

            container.Bind<IUnitTurnHandler, UnitTurnHandler>();
            container.Bind<UseAbilityOnTargetHandler>();
            container.Bind<IAbilityHandler, AttackAbilityHandler>();
            container.Bind<ISpellHandler, FireboltSpellHandler>();
            container.Bind<ISpellHandler, MinorRegenSpellHandler>();
        }
    }
}
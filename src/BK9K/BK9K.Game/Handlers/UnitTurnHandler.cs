using System.Linq;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.AI.Applicators.Models;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Handlers.Phases;
using BK9K.Game.Levels;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine.Extensions;

namespace BK9K.Game.Handlers
{
    public class UnitTurnHandler : IUnitTurnHandler
    {
        public Level Level { get; }
        public GameConfiguration Configuration { get; }
        public IEventSystem EventSystem { get; }
        public UseAbilityOnTargetHandler UseAbilityOnTargetHandler { get; }

        public UnitTurnHandler(Level level, GameConfiguration configuration, IEventSystem eventSystem, UseAbilityOnTargetHandler useAbilityOnTargetHandler)
        {
            Level = level;
            Configuration = configuration;
            EventSystem = eventSystem;
            UseAbilityOnTargetHandler = useAbilityOnTargetHandler;
        }

        public async Task TakeTurn(Unit unit)
        {
            if (ShouldStopTurn()) { return; }
            EventSystem.Publish(new UnitStartTurn(unit));
            await CheckAdvice(unit);
            EventSystem.Publish(new UnitEndTurnEvent(unit));
        }

        public async Task CheckAdvice(Unit unit)
        {
            var gameUnit = Level.GameUnits.Single(x => x.Unit.Id == unit.Id);
            var bestAdvice = gameUnit.Agent.GetAdvice().FirstOrDefault();

            if (bestAdvice == null) { return; }

            if (bestAdvice.AdviceId == AdviceVariableTypes.UseAbility)
            {
                var targetAndAbility = bestAdvice.GetRelatedContext() as AbilityWithTarget;
                if(targetAndAbility == null) { return; }

                var ability = unit.ActiveAbilities.FirstOrDefault(x => x.Id == targetAndAbility.AbilityId);
                if(ability == null) { return; }

                var target = Level.GameUnits.SingleOrDefault(x => x.Unit.Id == targetAndAbility.TargetUnitId);
                if(target == null) { return; }

                await UseAbilityOnTargetHandler.ExecutePhase(unit, target.Unit, ability);
            }
        }
        
        public bool ShouldStopTurn()
        {
            return (Level.HasLevelFinished || Level.IsLevelLoading) ;
        }
    }
}
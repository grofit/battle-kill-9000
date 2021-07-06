using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.AI.Applicators.Models;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Extensions;
using BK9K.Game.Handlers.Phases;
using BK9K.Game.Levels;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Extensions;
using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Types;

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
            var advice = gameUnit.Agent.GetAdvice().ToArray();
            var bestAdvice = advice.FirstOrDefault();

            if (bestAdvice == null) { return; }

            if (bestAdvice.AdviceId == AdviceVariableTypes.UseAbility)
            { await HandleUseAbilityAdvice(unit, bestAdvice); }
            else if(bestAdvice.AdviceId == AdviceVariableTypes.HealOther || bestAdvice.AdviceId == AdviceVariableTypes.HealSelf)
            { await HandleHealOtherAdvice(unit, bestAdvice); }
            else if (bestAdvice.AdviceId == AdviceVariableTypes.EscapeTo)
            { await HandleMovementFirstAdvice(unit, bestAdvice, advice); }
        }

        public async Task HandleMovementFirstAdvice(Unit unit, IAdvice movementAdvice, IAdvice[] allAdvice)
        {
            await HandleMovementAdvice(unit, movementAdvice);
            var attackOrHealAdvice = allAdvice.FirstOrDefault(x =>
                x.AdviceId == AdviceVariableTypes.HealSelf ||
                x.AdviceId == AdviceVariableTypes.HealOther ||
                x.AdviceId == AdviceVariableTypes.UseAbility);

            if (attackOrHealAdvice == null) { return; }

            if (attackOrHealAdvice.AdviceId == AdviceVariableTypes.HealSelf)
            { await HandleUseAbilityAdvice(unit, attackOrHealAdvice); }
            else if (attackOrHealAdvice.AdviceId == AdviceVariableTypes.HealOther)
            {
                var healTarget = attackOrHealAdvice.ContextAccessor.GetContext() as Unit;
                var healAbility = unit.ActiveAbilities.First(x => x.DamageType == DamageTypes.LightDamage);
                await UseAbilityOnTargetHandler.ExecuteIfInRange(unit, healTarget, healAbility);
            }
            else
            {
                var targetAndAbility = attackOrHealAdvice.ContextAccessor.GetContext() as AbilityWithTarget;
                var targetUnit = Level.GetUnitById(targetAndAbility.TargetUnitId);
                var ability = unit.GetAbility(targetAndAbility.AbilityId);
                await UseAbilityOnTargetHandler.ExecuteIfInRange(unit, targetUnit, ability);
            }
        }

        public async Task HandleUseAbilityAdvice(Unit unit, IAdvice advice)
        {
            var targetAndAbility = advice.ContextAccessor.GetContext() as AbilityWithTarget;
            if(targetAndAbility == null) { return; }

            var ability = unit.GetAbility(targetAndAbility.AbilityId);
            if(ability == null) { return; }

            var target = Level.GetUnitById(targetAndAbility.TargetUnitId);
            if(target == null) { return; }

            await UseAbilityOnTargetHandler.ExecutePhase(unit, target, ability);
        }
        
        public async Task HandleHealOtherAdvice(Unit unit, IAdvice advice)
        {
            var target = advice.ContextAccessor.GetContext() as Unit;
            if(target == null) { return; }

            var ability = unit.ActiveAbilities.FirstOrDefault(x => x.DamageType == DamageTypes.LightDamage);
            if(ability == null) { return; }

            await UseAbilityOnTargetHandler.ExecutePhase(unit, target, ability);
        }

        public async Task HandleMovementAdvice(Unit unit, IAdvice advice)
        {
            var bestLocation = (Vector2)advice.ContextAccessor.GetContext();
            unit.Position = bestLocation;
            EventSystem.Publish(new UnitMovingEvent(unit, unit.Position));
        }

        public bool ShouldStopTurn()
        {
            return (Level.HasLevelFinished || Level.IsLevelLoading) ;
        }
    }
}
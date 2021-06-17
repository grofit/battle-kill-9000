using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Configuration;
using BK9K.Game.Data.Repositories;
using BK9K.Game.Data.Variables;
using BK9K.Game.Events.Units;
using BK9K.Game.Levels;
using BK9K.Game.Movement;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Units;
using OpenRpg.Combat.Abilities;

namespace BK9K.Game.Handlers.Phases
{
    public class UseAbilityOnTargetHandler
    {
        public const int PhaseTime = 500;
        public int ScaledDelay => (int)(PhaseTime * GameConfiguration.GameSpeed);

        public GameConfiguration GameConfiguration { get; }
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        public MovementAdvisor MovementAdvisor { get; }
        public IAbilityHandlerRepository AbilityHandlerRepository { get; }
        
        public UseAbilityOnTargetHandler(IEventSystem eventSystem, GameConfiguration gameConfiguration, Level level, MovementAdvisor movementAdvisor, IAbilityHandlerRepository abilityHandlerRepository)
        {
            EventSystem = eventSystem;
            GameConfiguration = gameConfiguration;
            Level = level;
            MovementAdvisor = movementAdvisor;
            AbilityHandlerRepository = abilityHandlerRepository;
        }

        public async Task ExecutePhase(Unit unit, Unit target, UnitAbility ability)
        {
            await Task.Delay(ScaledDelay);

            var canHitEnemy = unit.IsUnitWithinRange(target, ability.Range);
            if (canHitEnemy)
            {
                await UseAbilityOn(unit, target, ability);
                return;
            }

            MoveTowards(unit, target);
            canHitEnemy = unit.IsUnitWithinRange(target, ability.Range);
            if (canHitEnemy)
            { await UseAbilityOn(unit, target, ability); }
        }

        public void MoveTowards(Unit unit, Unit target)
        {
            var bestMovement = MovementAdvisor.GetBestMovement(unit, target);
            unit.Position = bestMovement;
            EventSystem.Publish(new UnitMovingEvent(unit, unit.Position));
        }

        public async Task UseAbilityOn(Unit unit, Unit target, Ability ability)
        {
            var abilityToUse = AbilityHandlerRepository.Retrieve(unit.ActiveAbilities[0].Id);
            await abilityToUse.ExecuteAbility(unit, target);
        }
    }
}
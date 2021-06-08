using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Advisors;
using BK9K.UAI.Keys;

namespace BK9K.Game.AI
{
    public class AdviceGenerator
    {
        public IAdvice CreateHealSelfAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.HealSelf, new[]
            {
                new UtilityKey(UtilityVariableTypes.NeedsHealing)
            }, agent.GetRelatedUnit);
        }
        
        public IAdvice CreateHealOtherAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.HealOther, new[]
            {
                new UtilityKey(UtilityVariableTypes.NeedsHealing)
            }, agent.GetRelatedUnit);
        }

        public IAdvice CreateGoAttackAdvice(IAgent agent, Level level)
        {
            Unit GetBestEnemyTarget()
            {
                var closestEnemyUtility = agent.ConsiderationHandler.UtilityVariables
                    .GetRelatedUtilities(UtilityVariableTypes.EnemyDistance)
                    .OrderByDescending(x => x.Value)
                    .First();

                return level.GameUnits.Single(x => x.Unit.Id == closestEnemyUtility.Key.RelatedId).Unit;
            }

            return new DefaultAdvice(AdviceVariableTypes.GoAttack, new[]
            {
                new UtilityKey(UtilityVariableTypes.EnemyDistance)
            }, GetBestEnemyTarget);
        }
        
        public void PopulateAdvice(IAgent agent, Level level)
        {
            var unit = agent.GetRelatedUnit();

            if (unit.HasHealAbility())
            {
                var healSelfAdvice = CreateHealSelfAdvice(agent);
                agent.AdviceHandler.AddAdvice(healSelfAdvice);
            }
            
            if (unit.HasHealOtherAbility())
            {
                var healSelfAdvice = CreateHealSelfAdvice(agent);
                agent.AdviceHandler.AddAdvice(healSelfAdvice);
            }

            var goAttackAdvice = CreateGoAttackAdvice(agent, level);
            agent.AdviceHandler.AddAdvice(goAttackAdvice);
        }
    }
}
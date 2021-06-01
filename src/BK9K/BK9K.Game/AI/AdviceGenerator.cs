using System;
using System.Linq;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.UAI;
using BK9K.UAI.Advisors;
using BK9K.UAI.Keys;

namespace BK9K.Game.AI
{
    public class AdviceGenerator
    {
        public IAdvice CreateGoHealAdvice(IAgent agent)
        {
            return new DefaultAdvice(AdviceVariableTypes.GoHeal, new[]
            {
                new UtilityKey(UtilityVariableTypes.NeedsHealing)
            }, () => agent.GetRelatedUnit());
        }

        public IAdvice CreateGoAttackAdvice(IAgent agent, Level level)
        {
            GameUnit GetBestEnemyTarget()
            {
                var closestEnemyUtility = agent.ConsiderationHandler.UtilityVariables
                    .GetRelatedUtilities(UtilityVariableTypes.EnemyDistance)
                    .OrderBy(x => x.Value)
                    .First();

                return level.GameUnits.Single(x => x.Unit.Id == closestEnemyUtility.Key.RelatedId);
            }

            return new DefaultAdvice(AdviceVariableTypes.GoAttack, new[]
            {
                new UtilityKey(UtilityVariableTypes.EnemyDistance)
            }, (Func<GameUnit>) GetBestEnemyTarget);
        }
        
        public void PopulateAdvice(IAgent agent, Level level)
        {
            var goHealAdvice = CreateGoHealAdvice(agent);
            agent.AdviceHandler.AddAdvice(goHealAdvice);

            var goAttackAdvice = CreateGoAttackAdvice(agent, level);
            agent.AdviceHandler.AddAdvice(goAttackAdvice);
        }
    }
}
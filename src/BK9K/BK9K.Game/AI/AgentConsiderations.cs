using System.Collections.Generic;
using System.Numerics;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using BK9K.Mechanics.Variables;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Keys;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI
{
    public class AgentConsiderations
    {
        private static IClamper _distanceClamper = new Clamper(0, 5.0f);
        
        private IConsideration GetNeedsHealingConsideration(IAgent agent)
        {
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.Health(), () => agent.RelatedContext);
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetRelatedUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(healthValueAccessor, healthClamper, PresetEvaluators.InverseLinear);
        }

        private int GetOpposingFaction(int factionType)
        {
            return factionType == FactionTypes.Player ? FactionTypes.Enemy : FactionTypes.Player;
        }
        
        private void AddEnemyDistanceConsiderations(IAgent agent, Unit unit, List<Unit> allUnits)
        {
            var opposingFaction = GetOpposingFaction(unit.FactionType);
            var enemies = allUnits.GetAllUnitsInFaction(opposingFaction);
            foreach (var enemy in enemies)
            {
                var enemyDistanceAccessor =
                    new ManualValueAccessor(() => Vector2.Distance(enemy.Position, 
                        agent.GetRelatedUnit().Position), () => enemy);
                var enemyConsideration = new ValueBasedConsideration(enemyDistanceAccessor, _distanceClamper, PresetEvaluators.QuadraticLowerLeft);
                agent.AddConsideration(new UtilityKey(UtilityVariableTypes.EnemyDistance, enemy.Id), enemyConsideration);
            }
        }

        public void PopulateConsiderations(IAgent agent, Unit unit, List<Unit> allUnits)
        {
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing, GetNeedsHealingConsideration(agent));
            AddEnemyDistanceConsiderations(agent, unit, allUnits);
        }
    }
}
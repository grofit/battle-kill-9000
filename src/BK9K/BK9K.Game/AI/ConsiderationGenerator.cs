using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
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
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI
{
    public class ConsiderationGenerator
    {
        public IAttackGenerator AttackGenerator { get; }

        private static IClamper _distanceClamper = new Clamper(0, 5.0f);
        private static IClamper _damageClamper = new Clamper(0, 40);

        
        public ConsiderationGenerator(IAttackGenerator attackGenerator)
        {
            AttackGenerator = attackGenerator;
        }
        
        private IConsideration GetNeedsHealingConsideration(IAgent agent)
        {
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.Health(), () => agent.RelatedContext);
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetRelatedUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(healthValueAccessor, healthClamper, PresetEvaluators.InverseLinear);
        }
        
        private IConsideration GetIsPowerfulConsideration(IAgent agent)
        {
            var attackOutputAccessor = new ManualValueAccessor(() =>
                agent.GetRelatedUnit().Stats.GetDamageReferences().Sum(x => x.StatValue),
                () => agent.RelatedContext);
            return new ValueBasedConsideration(attackOutputAccessor, _damageClamper, PresetEvaluators.Linear);
        }
        
        private IConsideration GetIsDefensiveConsideration(IAgent agent)
        {
            var defenseOutputAccessor = new ManualValueAccessor(() => 
                agent.GetRelatedUnit().Stats.GetDefenseReferences().Sum(x => x.StatValue),
                () => agent.RelatedContext);
            return new ValueBasedConsideration(defenseOutputAccessor, _damageClamper, PresetEvaluators.Linear);
        }
        
        private IConsideration GetEnemyDistanceConsideration(IAgent agent, GameUnit enemy)
        {
            var enemyDistanceAccessor =
                new ManualValueAccessor(() => Vector2.Distance(enemy.Unit.Position, agent.GetRelatedUnit().Position), () => enemy);
            return new ValueBasedConsideration(enemyDistanceAccessor, _distanceClamper, PresetEvaluators.QuadraticLowerLeft);
        }

        public void PopulateLocalConsiderations(IAgent agent)
        {
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing, GetNeedsHealingConsideration(agent));
            agent.AddConsideration(UtilityVariableTypes.IsPowerful, GetIsPowerfulConsideration(agent));
            agent.AddConsideration(UtilityVariableTypes.IsDefensive, GetIsDefensiveConsideration(agent));
        }
        
        private int GetOpposingFaction(int factionType)
        {
            return factionType == FactionTypes.Player ? FactionTypes.Enemy : FactionTypes.Player;
        }
        
        public void PopulateExternalConsiderations(IAgent agent, Level level)
        {
            var localUnit = agent.GetRelatedUnit();
            var opposingFaction = GetOpposingFaction(localUnit.FactionType);
            var enemies = level.GetAllUnitsInFaction(opposingFaction);
            
            foreach (var enemy in enemies)
            {
                var enemyConsideration = GetEnemyDistanceConsideration(agent, enemy);
                var utilityKey = new UtilityKey(UtilityVariableTypes.EnemyDistance, enemy.Unit.Id);
                agent.AddConsideration(utilityKey, enemyConsideration);
            }
        }
    }
}
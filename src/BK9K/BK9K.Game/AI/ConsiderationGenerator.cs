using System.Linq;
using System.Numerics;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Types;
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
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.Health());
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetRelatedUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.NeedsHealing), healthValueAccessor, healthClamper, PresetEvaluators.InverseLinear);
        }
        
        private IConsideration GetIsPowerfulConsideration(IAgent agent)
        {
            var attackOutputAccessor = new ManualValueAccessor(() =>
                agent.GetRelatedUnit().Stats.GetDamageReferences().Sum(x => x.StatValue));
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsPowerful), attackOutputAccessor, _damageClamper, PresetEvaluators.Linear);
        }
        
        private IConsideration GetIsDefensiveConsideration(IAgent agent)
        {
            var defenseOutputAccessor = new ManualValueAccessor(() => 
                agent.GetRelatedUnit().Stats.GetDefenseReferences().Sum(x => x.StatValue));
            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsDefensive), defenseOutputAccessor, _damageClamper, PresetEvaluators.Linear);
        }
        
        private IConsideration GetEnemyDistanceConsideration(IAgent agent, GameUnit enemy)
        {
            var distanceUtilityKey = new UtilityKey(UtilityVariableTypes.EnemyDistance, enemy.Unit.Id);
            var enemyDistanceAccessor = new ManualValueAccessor(() => Vector2.Distance(enemy.Unit.Position, agent.GetRelatedUnit().Position));
            return new ValueBasedConsideration(distanceUtilityKey, enemyDistanceAccessor, _distanceClamper, PresetEvaluators.QuadraticLowerLeft);
        }

        private IConsideration IsInDangerConsideration(IAgent agent)
        {
            var dangerAccessor = new ManualValueAccessor(() =>
            {
                return agent.UtilityVariables
                    .GetRelatedUtilities(UtilityVariableTypes.IsADanger)
                    .Max(x => x.Value);
            });

            return new ValueBasedConsideration(new UtilityKey(UtilityVariableTypes.IsInDanger), dangerAccessor, PresetClampers.Passthrough, PresetEvaluators.PassThrough);
        }
        
        private IConsideration IsADangerConsideration(IAgent agent, GameUnit enemy)
        {
            var isDangerUtilityKey = new UtilityKey(UtilityVariableTypes.IsADanger, enemy.Unit.Id);
            var isADangerAccessor = new ManualValueAccessor(() =>
            {
                var utilityKey = new UtilityKey(UtilityVariableTypes.EnemyDistance, enemy.Unit.Id);
                if (!agent.UtilityVariables.ContainsKey(utilityKey))
                { return 0.0f; }

                var distanceUtility = agent.UtilityVariables[utilityKey];
                var powerfulUtility = enemy.Agent.UtilityVariables[UtilityVariableTypes.IsPowerful];
                return UtilityExtensions.CalculateScore(distanceUtility, powerfulUtility);
            });
            
            return new ValueBasedConsideration(isDangerUtilityKey, isADangerAccessor, PresetClampers.Passthrough, PresetEvaluators.PassThrough);
        }

        public void PopulateLocalConsiderations(IAgent agent)
        {
            agent.AddConsideration(GetNeedsHealingConsideration(agent));
            agent.AddConsideration(GetIsPowerfulConsideration(agent));
            agent.AddConsideration(GetIsDefensiveConsideration(agent));
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
                var enemyDistanceConsideration = GetEnemyDistanceConsideration(agent, enemy);
                agent.AddConsideration(enemyDistanceConsideration);

                var enemyIsDangerConsideration = IsADangerConsideration(agent, enemy);
                agent.AddConsideration(enemyIsDangerConsideration);
            }

            var isInDangerConsideration = IsInDangerConsideration(agent);
            agent.AddConsideration(isInDangerConsideration);
        }
    }
}
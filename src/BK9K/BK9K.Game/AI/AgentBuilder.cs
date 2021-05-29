using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemsRx.Events;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Scheduler;
using BK9K.Mechanics.Units;
using BK9K.Mechanics.Variables;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Handlers;
using BK9K.UAI.Keys;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI
{
    public class AgentBuilder
    {
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        
        private static IClamper _distanceClamper = new Clamper(0, 5.0f);

        
        private Unit _relatedUnit;

        public AgentBuilder(IEventSystem eventSystem, Level level)
        {
            EventSystem = eventSystem;
            Level = level;
        }

        public AgentBuilder Create()
        { return new(EventSystem, Level); }

        public AgentBuilder ForUnit(Unit relatedUnit)
        {
            _relatedUnit = relatedUnit;
            return this;
        }

        public IAgent Build()
        {
            if (_relatedUnit == null)
            { throw new Exception("AgentBuilder requires a unit to build for"); }

            if (_relatedUnit.Id == 0)
            { throw new Exception("Unit must have a valid Id"); }
            
            var agent = new Agent(_relatedUnit, new ConsiderationHandler(new CustomConsiderationScheduler(EventSystem)));
            PopulateConsiderations(agent);
            return agent;
        }

        private IConsideration GetNeedsHealingConsideration(IAgent agent)
        {
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.Health(), () => agent.RelatedContext);
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetRelatedUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(healthValueAccessor, healthClamper, PresetEvaluators.InverseLinear);
        }

        private void AddEnemyDistanceConsiderations(IAgent agent)
        {
            var enemies = Level.GetAllEnemies();
            foreach (var enemy in enemies)
            {
                var enemyDistanceAccessor =
                    new ManualValueAccessor(() => Vector2.Distance(enemy.Unit.Position, 
                        agent.GetRelatedUnit().Position), () => enemy);
                var enemyConsideration = new ValueBasedConsideration(enemyDistanceAccessor, _distanceClamper, PresetEvaluators.QuadraticLowerLeft);
                agent.AddConsideration(new UtilityKey(UtilityVariableTypes.EnemyDistance, enemy.Unit.Id), enemyConsideration);
            }
        }

        private void PopulateConsiderations(IAgent agent)
        {
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing, GetNeedsHealingConsideration(agent));
            AddEnemyDistanceConsiderations(agent);
        }
    }
}
using System.Linq;
using SystemsRx.Events;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Game.Scheduler;
using BK9K.Game.Variables;
using BK9K.Mechanics.Units;
using BK9K.UAI;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Handlers;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.AI
{
    public class AgentBuilder
    {
        public IEventSystem EventSystem { get; }
        public Level Level { get; }
        
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
            var agent = new Agent(_relatedUnit, new ConsiderationHandler(new CustomConsiderationScheduler(EventSystem)));
            PopulateConsiderations(agent);
            return agent;
        }

        public IConsideration GetNeedsHealingConsideration(IAgent agent)
        {
            var healthValueAccessor = new ManualValueAccessor(() => agent.GetRelatedUnit().Stats.Health(), () => agent.RelatedContext);
            var healthClamper = new DynamicClamper(() => 0, () => agent.GetRelatedUnit().Stats.MaxHealth());
            return new ValueBasedConsideration(healthValueAccessor, healthClamper, PresetEvaluators.QuadraticLowerLeft);
        }

        public IConsideration HasEnemiesWithinRangeConsideration(IAgent agent)
        {
            var enemies = Level.GetAllEnemies();
            foreach (var enemy in enemies)
            {
                
            }

            return null;
        }

        public void PopulateConsiderations(IAgent agent)
        {
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing, GetNeedsHealingConsideration(agent));
        }
    }
}
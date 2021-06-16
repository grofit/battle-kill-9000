using System.Collections.Generic;
using System.Linq;
using BK9K.Game.Levels;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Considerations;
using OpenRpg.AdviceEngine.Considerations.Applicators;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Considerations.External
{
    public abstract class LevelExternalConsiderationApplicator : DefaultExternalConsiderationApplicator<Unit>
    {
        public Level Level { get; }

        protected LevelExternalConsiderationApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker)
        { Level = level; }

        public override IEnumerable<IConsideration> CreateConsiderations(IAgent agent)
        {
            var otherUnits = Level.GameUnits.Where(x => x.Agent != agent);
            foreach (var otherUnit in otherUnits)
            {
                if(ShouldConsiderUnit(agent, otherUnit))
                { yield return CreateConsideration(agent, otherUnit); } 
            }
        }

        public abstract bool ShouldConsiderUnit(IAgent agent, GameUnit otherUnit);
        public abstract IConsideration CreateConsideration(IAgent agent, GameUnit otherUnit);
    }
}
using System;
using System.Threading.Tasks;
using BK9K.Game.AI.Applicators;
using BK9K.Game.Processors;
using BK9K.UAI.Advisors.Applicators.Registries;
using BK9K.UAI.Considerations.Applicators.Registries;

namespace BK9K.Game.Levels.Processors
{
    public class LevelAgentSetupProcessor : IProcessor<Level>
    {
        public int Priority => 6;
        
        public IConsiderationApplicatorRegistry ConsiderationApplicatorRegistry { get; }
        public IAdviceApplicatorRegistry AdviceApplicatorRegistry { get; }

        public LevelAgentSetupProcessor(IConsiderationApplicatorRegistry considerationApplicatorRegistry, IAdviceApplicatorRegistry adviceApplicatorRegistry)
        {
            ConsiderationApplicatorRegistry = considerationApplicatorRegistry;
            AdviceApplicatorRegistry = adviceApplicatorRegistry;
        }

        public Task Process(Level context)
        {
            ApplyConsiderationsForPriority(context, ApplicatorPriorities.Local);
            ApplyConsiderationsForPriority(context, ApplicatorPriorities.DependenciesOnLocal);
            ApplyConsiderationsForPriority(context, ApplicatorPriorities.DependenciesOnExternal);
            ApplyAdvice(context);
            return Task.CompletedTask;
        }

        public void ApplyConsiderationsForPriority(Level level, int priorityLevel)
        {
            foreach (var gameUnit in level.GameUnits)
            {
                ConsiderationApplicatorRegistry.ApplyOnlyPriority(gameUnit.Agent, priorityLevel);
            }
        }
        
        public void ApplyAdvice(Level level)
        {
            foreach (var gameUnit in level.GameUnits)
            {
                AdviceApplicatorRegistry.ApplyAll(gameUnit.Agent);
            }
        }
    }
}
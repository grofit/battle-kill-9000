using System.Threading.Tasks;
using BK9K.Game.AI.Applicators;
using BK9K.Game.AI.Service;
using BK9K.Game.Processors;

namespace BK9K.Game.Levels.Processors
{
    public class LevelAgentSetupProcessor : IProcessor<Level>
    {
        public int Priority => 6;
        
        public IAgentService AgentService { get; }

        public LevelAgentSetupProcessor(IAgentService agentService)
        {
            AgentService = agentService;
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
                AgentService.ApplyConsiderationsForPriority(gameUnit.Agent, priorityLevel);
            }
        }
        
        public void ApplyAdvice(Level level)
        {
            foreach (var gameUnit in level.GameUnits)
            {
                AgentService.ApplyAdvice(gameUnit.Agent);
            }
        }
    }
}
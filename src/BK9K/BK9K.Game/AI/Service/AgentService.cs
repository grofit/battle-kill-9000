using System.Threading.Tasks;
using BK9K.Game.AI.Applicators;
using BK9K.Game.Levels;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Advisors.Applicators.Registries;
using OpenRpg.AdviceEngine.Considerations.Applicators.Registries;

namespace BK9K.Game.AI.Service
{
    public class AgentService : IAgentService
    {
        public IConsiderationApplicatorRegistry ConsiderationApplicatorRegistry { get; }
        public IAdviceApplicatorRegistry AdviceApplicatorRegistry { get; }

        public AgentService(IConsiderationApplicatorRegistry considerationApplicatorRegistry, IAdviceApplicatorRegistry adviceApplicatorRegistry)
        {
            ConsiderationApplicatorRegistry = considerationApplicatorRegistry;
            AdviceApplicatorRegistry = adviceApplicatorRegistry;
        }

        public void RefreshAgent(IAgent agent)
        {
            agent.AdviceHandler.ClearAdvice();
            agent.ConsiderationHandler.ClearConsiderations();
            ApplyConsiderationsForPriority(agent, ApplicatorPriorities.Local);
            ApplyConsiderationsForPriority(agent, ApplicatorPriorities.DependenciesOnLocal);
            ApplyConsiderationsForPriority(agent, ApplicatorPriorities.DependenciesOnExternal);
            ApplyAdvice(agent);
        }

        public void ApplyConsiderationsForPriority(IAgent agent, int priorityLevel)
        { ConsiderationApplicatorRegistry.ApplyOnlyPriority(agent, priorityLevel); }

        public void ApplyAdvice(IAgent agent)
        { AdviceApplicatorRegistry.ApplyAll(agent); }
    }
}
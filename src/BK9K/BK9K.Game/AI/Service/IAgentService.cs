using BK9K.Game.Levels;
using OpenRpg.AdviceEngine;

namespace BK9K.Game.AI.Service
{
    public interface IAgentService
    {
        public void RefreshAgent(IAgent agent);
        void ApplyConsiderationsForPriority(IAgent agent, int priorityLevel);
        void ApplyAdvice(IAgent agent);
    }
}
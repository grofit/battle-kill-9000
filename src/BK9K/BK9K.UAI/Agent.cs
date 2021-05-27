using BK9K.UAI.Handlers;
using BK9K.UAI.Variables;

namespace BK9K.UAI
{
    public class Agent : IAgent
    {
        public IUtilityVariables UtilityVariables { get; } = new UtilityVariables();
        public IConsiderationHandler ConsiderationHandler { get; }

        public Agent(IConsiderationHandler handler)
        {
            ConsiderationHandler = handler;
            ConsiderationHandler.StartHandler(UtilityVariables);
        }

        public void Dispose()
        {
            ConsiderationHandler.Dispose();
        }
    }
}
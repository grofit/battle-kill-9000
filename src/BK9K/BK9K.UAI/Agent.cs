using BK9K.UAI.Handlers;
using BK9K.UAI.Handlers.Considerations;
using BK9K.UAI.Variables;

namespace BK9K.UAI
{
    public class Agent : IAgent
    {
        public object RelatedContext { get; set; }
        public IUtilityVariables UtilityVariables { get; } = new UtilityVariables();
        public IConsiderationHandler ConsiderationHandler { get; }

        public Agent(object relatedContext, IConsiderationHandler handler)
        {
            RelatedContext = relatedContext;
            ConsiderationHandler = handler;
            ConsiderationHandler.StartHandler(UtilityVariables);
        }

        public void Dispose()
        {
            ConsiderationHandler.Dispose();
        }
    }
}
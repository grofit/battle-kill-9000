using BK9K.UAI.Handlers.Advisors;
using BK9K.UAI.Handlers.Considerations;
using BK9K.UAI.Variables;
using OpenRpg.Core.Common;

namespace BK9K.UAI
{
    public class Agent : IAgent
    {
        public IHasDataId RelatedContext { get; }
        public IUtilityVariables UtilityVariables { get; } = new UtilityVariables();
        public IConsiderationHandler ConsiderationHandler { get; }
        public IAdviceHandler AdviceHandler { get; }

        public Agent(IHasDataId relatedContext, IConsiderationHandler handler, IAdviceHandler adviceHandler)
        {
            RelatedContext = relatedContext;
            ConsiderationHandler = handler;
            AdviceHandler = adviceHandler;
            ConsiderationHandler.StartHandler(UtilityVariables);
            AdviceHandler.StartHandler(UtilityVariables);
        }

        public void Dispose()
        {
            ConsiderationHandler.Dispose();
            AdviceHandler.Dispose();
        }
    }
}
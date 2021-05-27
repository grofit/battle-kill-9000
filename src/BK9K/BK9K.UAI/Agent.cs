using BK9K.UAI.Variables;

namespace BK9K.UAI
{
    public class Agent : IAgent
    {
        public IConsiderationVariables Considerations { get; } = new ConsiderationVariables();
    }
}
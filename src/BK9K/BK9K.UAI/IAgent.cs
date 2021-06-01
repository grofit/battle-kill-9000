using System;
using BK9K.UAI.Handlers;
using BK9K.UAI.Handlers.Considerations;
using BK9K.UAI.Variables;

namespace BK9K.UAI
{
    public interface IAgent : IDisposable
    {
        object RelatedContext { get; }
        IUtilityVariables UtilityVariables { get; }
        IConsiderationHandler ConsiderationHandler { get; }
    }
}
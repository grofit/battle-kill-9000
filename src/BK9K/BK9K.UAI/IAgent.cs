using System;
using BK9K.UAI.Handlers;
using BK9K.UAI.Handlers.Advisors;
using BK9K.UAI.Handlers.Considerations;
using BK9K.UAI.Variables;
using OpenRpg.Core.Common;

namespace BK9K.UAI
{
    public interface IAgent : IDisposable
    {
        IHasDataId RelatedContext { get; }
        IUtilityVariables UtilityVariables { get; }
        IConsiderationHandler ConsiderationHandler { get; }
        IAdviceHandler AdviceHandler { get; }
    }
}
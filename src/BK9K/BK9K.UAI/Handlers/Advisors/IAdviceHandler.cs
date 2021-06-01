using System;
using BK9K.UAI.Advisors;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Handlers.Advisors
{
    public interface IAdviceHandler : IDisposable
    {
        IUtilityVariables UtilityVariables { get; }

        void StartHandler(IUtilityVariables variables);
        void StopHandler();
        
        void AddAdvice(IAdvice advice);
        void RemoveAdvice(IAdvice advice);
    }
}
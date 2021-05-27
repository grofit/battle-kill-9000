using System;
using System.Reactive;
using BK9K.UAI.Considerations;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Handlers
{
    public interface IConsiderationHandler : IDisposable
    {
        IUtilityVariables UtilityVariables { get; }

        void StartHandler(IUtilityVariables variables);
        void StopHandler();
        
        void AddConsideration(int utilityId, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null);
        void RemoveConsideration(int utilityId);
    }
}
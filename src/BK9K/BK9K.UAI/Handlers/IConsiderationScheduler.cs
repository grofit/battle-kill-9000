using System;
using System.Reactive;

namespace BK9K.UAI.Handlers
{
    public interface IConsiderationScheduler
    {
        IObservable<Unit> DefaultRefreshPeriod { get; }
    }
}
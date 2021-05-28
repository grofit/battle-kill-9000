using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using BK9K.UAI.Considerations;
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;
using OpenRpg.Core.Variables;

namespace BK9K.UAI.Handlers
{
    public class ConsiderationHandler : IDisposable, IConsiderationHandler
    {
        public IUtilityVariables UtilityVariables { get; protected set; }
        private bool _isRunning = false;

        private readonly IDictionary<UtilityKey, IConsideration> _considerations = new Dictionary<UtilityKey, IConsideration>();
        private readonly IDictionary<UtilityKey, IDisposable> _explicitUpdateSchedules = new Dictionary<UtilityKey, IDisposable>();
        private readonly IList<UtilityKey> _generalUpdateConsiderations = new List<UtilityKey>();
        private readonly IDisposable _generalUpdateSub;
        
        public ConsiderationHandler(IConsiderationScheduler scheduler)
        { _generalUpdateSub = scheduler.DefaultRefreshPeriod.Subscribe(x => GeneralRefreshConsiderations()); }
        
        public void StartHandler(IUtilityVariables variables)
        {
            _isRunning = true;
            UtilityVariables = variables;
        }

        public void StopHandler()
        {
            _isRunning = false;
        }
        
        public void AddConsideration(UtilityKey utilityKey, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            _considerations.Add(utilityKey, consideration);
            UtilityVariables[utilityKey] = 0;
            HandleSchedulingForConsideration(utilityKey, explicitUpdateTrigger);
            RefreshConsideration(utilityKey);
        }

        public void RemoveConsideration(UtilityKey utilityKey)
        {
            if (_explicitUpdateSchedules.ContainsKey(utilityKey))
            {
                _explicitUpdateSchedules[utilityKey].Dispose();
                _explicitUpdateSchedules.Remove(utilityKey);
            }

            if (_generalUpdateConsiderations.Contains(utilityKey))
            { _generalUpdateConsiderations.Remove(utilityKey); }

            _considerations.Remove(utilityKey);
            UtilityVariables.RemoveVariable(utilityKey);
        }

        private void HandleSchedulingForConsideration(UtilityKey utilityKey, IObservable<Unit> explicitUpdateTrigger = null)
        {
            if (explicitUpdateTrigger != null)
            {
                var sub = explicitUpdateTrigger.Subscribe(x => RefreshConsideration(utilityKey));
                _explicitUpdateSchedules[utilityKey] = sub;
                return;
            }
            
            if (_considerations[utilityKey] is IValueBasedConsideration valueConsideration)
            { HandleDefaultSchedulingForValueConsideration(utilityKey, valueConsideration); }
            else if(_considerations[utilityKey] is IUtilityBasedConsideration utilityConsideration)
            { HandleDefaultSchedulingForUtilityConsideration(utilityKey, utilityConsideration); }
            
        }

        private void HandleDefaultSchedulingForValueConsideration(UtilityKey utilityKey, IValueBasedConsideration consideration)
        {
            _generalUpdateConsiderations.Add(utilityKey);
        }
        
        private void HandleDefaultSchedulingForUtilityConsideration(UtilityKey utilityKey, IUtilityBasedConsideration consideration)
        {
            var sub = Observable
                .FromEventPattern<VariableChangedEventHandler<UtilityKey, float>, VariableChangedEventArgs<UtilityKey, float>>(
                    x => UtilityVariables.OnVariableChanged += x,
                    x => UtilityVariables.OnVariableChanged -= x)
                .SingleAsync(x => x.EventArgs.VariableType.Equals(utilityKey))
                .Subscribe(x => RefreshConsideration(x.EventArgs.VariableType));

            _explicitUpdateSchedules[utilityKey] = sub;
        }

        private void RefreshConsideration(UtilityKey utilityKey)
        {
            if(!_isRunning) { return; }
            
            var consideration = _considerations[utilityKey];
            var newUtility = consideration.CalculateUtility(UtilityVariables);
            UtilityVariables[utilityKey] = newUtility;
        }

        private void GeneralRefreshConsiderations()
        {
            foreach (var utilityId in _generalUpdateConsiderations)
            { RefreshConsideration(utilityId); }
        }

        public void Dispose()
        {
            foreach (var disposable in _explicitUpdateSchedules.Values)
            { disposable.Dispose(); }
            
            _generalUpdateSub.Dispose();
        }
    }
}
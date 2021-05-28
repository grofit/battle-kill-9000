using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using BK9K.UAI.Considerations;
using BK9K.UAI.Variables;
using OpenRpg.Core.Variables;

namespace BK9K.UAI.Handlers
{
    public class ConsiderationHandler : IDisposable, IConsiderationHandler
    {
        public IUtilityVariables UtilityVariables { get; protected set; }
        private bool _isRunning = false;

        private readonly IDictionary<int, IConsideration> _considerations = new Dictionary<int, IConsideration>();
        private readonly IDictionary<int, IDisposable> _explicitUpdateSchedules = new Dictionary<int, IDisposable>();
        private readonly IList<int> _generalUpdateConsiderations = new List<int>();
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
        
        public void AddConsideration(int utilityId, IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            _considerations.Add(utilityId, consideration);
            UtilityVariables[utilityId] = 0;
            HandleSchedulingForConsideration(utilityId, explicitUpdateTrigger);
            RefreshConsideration(utilityId);
        }

        public void RemoveConsideration(int utilityId)
        {
            if (_explicitUpdateSchedules.ContainsKey(utilityId))
            {
                _explicitUpdateSchedules[utilityId].Dispose();
                _explicitUpdateSchedules.Remove(utilityId);
            }

            if (_generalUpdateConsiderations.Contains(utilityId))
            { _generalUpdateConsiderations.Remove(utilityId); }

            _considerations.Remove(utilityId);
            UtilityVariables.RemoveVariable(utilityId);
        }

        private void HandleSchedulingForConsideration(int utilityId, IObservable<Unit> explicitUpdateTrigger = null)
        {
            if (explicitUpdateTrigger != null)
            {
                var sub = explicitUpdateTrigger.Subscribe(x => RefreshConsideration(utilityId));
                _explicitUpdateSchedules[utilityId] = sub;
                return;
            }
            
            if (_considerations[utilityId] is IValueBasedConsideration valueConsideration)
            { HandleDefaultSchedulingForValueConsideration(utilityId, valueConsideration); }
            else if(_considerations[utilityId] is IUtilityBasedConsideration utilityConsideration)
            { HandleDefaultSchedulingForUtilityConsideration(utilityId, utilityConsideration); }
            
        }

        private void HandleDefaultSchedulingForValueConsideration(int utilityId, IValueBasedConsideration consideration)
        {
            _generalUpdateConsiderations.Add(utilityId);
        }
        
        private void HandleDefaultSchedulingForUtilityConsideration(int utilityId, IUtilityBasedConsideration consideration)
        {
            var sub = Observable
                .FromEventPattern<VariableChangedEventHandler<int, float>, VariableChangedEventArgs<int, float>>(
                    x => UtilityVariables.OnVariableChanged += x,
                    x => UtilityVariables.OnVariableChanged -= x)
                .Where(x => x.EventArgs.VariableType == utilityId)
                .Subscribe(x => RefreshConsideration(x.EventArgs.VariableType));

            _explicitUpdateSchedules[utilityId] = sub;
        }

        private void RefreshConsideration(int utilityId)
        {
            if(!_isRunning) { return; }
            
            var consideration = _considerations[utilityId];
            var newUtility = consideration.CalculateUtility(UtilityVariables);
            UtilityVariables[utilityId] = newUtility;
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
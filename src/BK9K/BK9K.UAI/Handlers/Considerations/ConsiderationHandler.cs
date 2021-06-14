using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using SystemsRx.Extensions;
using BK9K.UAI.Considerations;
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;
using EcsRx.MicroRx.Disposables;
using OpenRpg.Core.Variables;

namespace BK9K.UAI.Handlers.Considerations
{
    public class ConsiderationHandler : IConsiderationHandler
    {
        public IUtilityVariables UtilityVariables { get; protected set; }
        private bool _isRunning = false;

        private readonly IDictionary<UtilityKey, IConsideration> _considerations = new Dictionary<UtilityKey, IConsideration>();
        private readonly IDictionary<UtilityKey, IDisposable> _explicitUpdateSchedules = new Dictionary<UtilityKey, IDisposable>();
        private readonly IList<UtilityKey> _generalUpdateConsiderations = new List<UtilityKey>();
        private readonly IDisposable _generalUpdateSub;
        
        public ConsiderationHandler(IRefreshScheduler scheduler)
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
        
        public void AddConsideration(IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            _considerations.Add(consideration.UtilityId, consideration);
            UtilityVariables[consideration.UtilityId] = 0;
            HandleSchedulingForConsideration(consideration, explicitUpdateTrigger);
            RefreshConsideration(consideration);
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

        private void HandleSchedulingForConsideration(IConsideration consideration, IObservable<Unit> explicitUpdateTrigger = null)
        {
            if (explicitUpdateTrigger != null)
            {
                var sub = explicitUpdateTrigger.Subscribe(x => RefreshConsideration(consideration));
                _explicitUpdateSchedules[consideration.UtilityId] = sub;
                return;
            }
            
            if (_considerations[consideration.UtilityId] is IValueBasedConsideration valueConsideration)
            { HandleDefaultSchedulingForValueConsideration(valueConsideration); }
            else if(_considerations[consideration.UtilityId] is IUtilityBasedConsideration utilityConsideration)
            { HandleDefaultSchedulingForUtilityConsideration(utilityConsideration); }
            
        }

        private void HandleDefaultSchedulingForValueConsideration(IValueBasedConsideration consideration)
        {
            _generalUpdateConsiderations.Add(consideration.UtilityId);
        }
        
        private void HandleDefaultSchedulingForUtilityConsideration(IUtilityBasedConsideration consideration)
        {
            var compositeDisposable = new CompositeDisposable();
            Observable.FromEventPattern<VariableChangedEventHandler<UtilityKey, float>, VariableEventArgs<UtilityKey, float>>(
                    x => UtilityVariables.OnVariableChanged += x,
                    x => UtilityVariables.OnVariableChanged -= x)
                .SingleAsync(x => x.EventArgs.VariableType.Equals(consideration.DependentUtilityId))
                .Subscribe(x => RefreshConsideration(consideration))
                .AddTo(compositeDisposable);
            
            Observable.FromEventPattern<VariableChangedEventHandler<UtilityKey, float>, VariableEventArgs<UtilityKey, float>>(
                x => UtilityVariables.OnVariableRemoved += x,
                x => UtilityVariables.OnVariableRemoved -= x)
                .FirstAsync()
                .Subscribe(x => RemoveConsideration(consideration.UtilityId))
                .AddTo(compositeDisposable);

            _explicitUpdateSchedules[consideration.UtilityId] = compositeDisposable;
        }

        private void RefreshConsideration(IConsideration consideration)
        {
            if(!_isRunning) { return; }
            
            var newUtility = 0.0f;
            if (consideration is ExternalUtilityBasedConsideration externalUtilityBasedConsideration)
            {
                var externalVariables = externalUtilityBasedConsideration.ExternalVariableAccessor();
                newUtility = consideration.CalculateUtility(externalVariables);
            }
            else
            { newUtility = consideration.CalculateUtility(UtilityVariables); }
            UtilityVariables[consideration.UtilityId] = newUtility;
        }

        private void GeneralRefreshConsiderations()
        {
            foreach (var utilityId in _generalUpdateConsiderations)
            { RefreshConsideration(_considerations[utilityId]); }
        }

        public void Dispose()
        {
            foreach (var disposable in _explicitUpdateSchedules.Values)
            { disposable.Dispose(); }
            
            _generalUpdateSub.Dispose();
        }
    }
}
using System;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Scheduling;
using EcsRx.Systems;

namespace BK9K.Game.Systems.Paradigms
{
    public abstract class UpdateSystem : IManualSystem, IDisposable
    {
        public IGroup Group { get; } = new EmptyGroup();
        public IUpdateScheduler UpdateScheduler { get; }

        private IDisposable updateSub;

        protected UpdateSystem(IUpdateScheduler updateScheduler)
        {
            UpdateScheduler = updateScheduler;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
            updateSub = UpdateScheduler.OnUpdate.Subscribe(OnUpdate);
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            Dispose();
        }

        public abstract void OnUpdate(ElapsedTime elapsed);
        
        public void Dispose()
        {
            updateSub?.Dispose();
        }
    }
}
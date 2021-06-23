using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Units;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Levelling;

namespace BK9K.Game.Systems.Combat
{
    public class ExperienceAllocationSystem : IReactToEventSystem<UnitHasDiedEvent>
    {
        public IExperienceCalculator ExperienceCalculator { get; set; }
        public IEventSystem EventSystem { get; set; }

        public ExperienceAllocationSystem(IExperienceCalculator experienceCalculator, IEventSystem eventSystem)
        {
            ExperienceCalculator = experienceCalculator;
            EventSystem = eventSystem;
        }

        public void Process(UnitHasDiedEvent eventData)
        {
            var receivingUnit = eventData.Killer;
            receivingUnit.Stats.AddExperience(eventData.Target.Stats.Experience());
            
            var nextLevelThreshold = ExperienceCalculator.GetExperienceRequiredForLevel(receivingUnit.Class.Level+1);
            if (receivingUnit.Stats.Experience() >= nextLevelThreshold)
            {
                receivingUnit.Class.Level++;
                receivingUnit.Stats.RemoveExperience(nextLevelThreshold);
                EventSystem.Publish(new UnitLeveledUpEvent(receivingUnit));
            }
        }
    }
}
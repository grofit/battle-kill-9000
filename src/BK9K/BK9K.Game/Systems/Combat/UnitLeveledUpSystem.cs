using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Units;
using BK9K.Mechanics.Classes;
using BK9K.Mechanics.Extensions;

namespace BK9K.Game.Systems.Combat
{
    public class UnitLeveledUpSystem : IReactToEventSystem<UnitLeveledUpEvent>
    {
        public void Process(UnitLeveledUpEvent eventData)
        {
            var unitToLevel = eventData.Unit;
            var currentClass = unitToLevel.Class.ClassTemplate as ICustomClassTemplate;
            var levelUpEffects = currentClass.LevelUpEffects;
            unitToLevel.AddOrApplyPassiveEffects(levelUpEffects);
        }
    }
}
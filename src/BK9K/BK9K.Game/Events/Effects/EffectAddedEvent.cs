using BK9K.Mechanics.Units;
using OpenRpg.Combat.Effects;

namespace BK9K.Game.Events.Effects
{
    public class EffectAddedEvent
    {
        public Unit Unit { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
    }
}
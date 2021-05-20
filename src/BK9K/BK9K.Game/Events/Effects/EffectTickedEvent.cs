using BK9K.Framework.Units;
using OpenRpg.Combat.Effects;

namespace BK9K.Game.Events.Effects
{
    public class EffectTickedEvent
    {
        public Unit Unit { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
        public int TickNumber { get; set; }
    }
}
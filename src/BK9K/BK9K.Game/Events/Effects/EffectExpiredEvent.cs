using BK9K.Framework.Units;
using OpenRpg.Combat.Effects;

namespace BK9K.Game.Events.Effects
{
    public class EffectExpiredEvent
    {
        public Unit Unit { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
    }
}
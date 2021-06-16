using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;

namespace BK9K.Game.Units
{
    public class GameUnit
    {
        public Unit Unit { get; }
        public IAgent Agent { get; }

        public GameUnit(Unit unit, IAgent agent)
        {
            Unit = unit;
            Agent = agent;
        }
    }
}
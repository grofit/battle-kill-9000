using System.Collections.Generic;
using BK9K.Game.AI;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Extensions
{
    public static class AgentFactoryExtensions
    {
        public static IEnumerable<GameUnit> GenerateGameUnits(this AgentFactory factory, IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                var agent = factory.CreateFor(unit);
                yield return new GameUnit(unit, agent);
            }
        }
    }
}
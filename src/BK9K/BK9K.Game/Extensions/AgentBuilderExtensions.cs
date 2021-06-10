using System.Collections.Generic;
using BK9K.Game.AI;
using BK9K.Game.Units;
using BK9K.Mechanics.Units;

namespace BK9K.Game.Extensions
{
    public static class AgentBuilderExtensions
    {
        public static IEnumerable<GameUnit> GenerateGameUnits(this AgentBuilder builder, IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                var agent = builder.Create()
                    .ForUnit(unit)
                    .Build();
            
                yield return new GameUnit(unit, agent);
            }
        }
    }
}
using BK9K.Mechanics.Types;
using OpenRpg.Core.Stats;

namespace BK9K.Mechanics.Extensions
{
    public static class StatsExtensions
    {
        public static int Initiative(this IStatsVariables stats)
        { return (int)stats[GameStatsVariableTypes.Initiative]; }

        public static float Initiative(this IStatsVariables stats, int initiative)
        { return stats[GameStatsVariableTypes.Initiative] = initiative; }
    }
}
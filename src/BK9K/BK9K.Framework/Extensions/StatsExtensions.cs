using BK9K.Framework.Types;
using OpenRpg.Core.Stats;

namespace BK9K.Framework.Extensions
{
    public static class StatsExtensions
    {
        public static int Initiative(this IStatsVariables stats)
        { return (int)stats[StatsVariableTypes.Initiative]; }

        public static float Initiative(this IStatsVariables stats, int initiative)
        { return stats[StatsVariableTypes.Initiative] = initiative; }
    }
}
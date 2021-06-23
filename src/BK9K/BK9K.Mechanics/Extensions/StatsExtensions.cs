using BK9K.Mechanics.Types;
using OpenRpg.Core.Stats;

namespace BK9K.Mechanics.Extensions
{
    public static class StatsExtensions
    {
        public static int Initiative(this IStatsVariables stats)
        { return (int)stats.GetVariable(GameStatsVariableTypes.Initiative); }

        public static void Initiative(this IStatsVariables stats, int initiative)
        { stats[GameStatsVariableTypes.Initiative] = initiative; }
        
        public static int Experience(this IStatsVariables stats)
        { return (int)stats.GetVariable(GameStatsVariableTypes.Experience); }
        
        public static void SetExperience(this IStatsVariables stats, int experience)
        { stats[GameStatsVariableTypes.Experience] = experience; }

        public static void AddExperience(this IStatsVariables stats, int experience)
        {
            var currentExperience = stats.GetVariable(GameStatsVariableTypes.Experience);
            stats[GameStatsVariableTypes.Experience] = (int)(currentExperience + experience);
        }
        
        public static void RemoveExperience(this IStatsVariables stats, int experience)
        {
            var currentExperience = stats.GetVariable(GameStatsVariableTypes.Experience);
            stats[GameStatsVariableTypes.Experience] = (int)(currentExperience - experience);
        }
    }
}
using BK9K.UAI.Tester.Variables.Types;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Tester.Variables.Extensions
{
    public static class IUtilityVariablesExtensions
    {
        public static float NeedsHealing(this IUtilityVariables utilities) => utilities[UtilityVariableTypes.NeedsHealing];
        public static float NeedsHealing(this IUtilityVariables utilities, float utility) => utilities[UtilityVariableTypes.NeedsHealing] = utility;
        
    }
}
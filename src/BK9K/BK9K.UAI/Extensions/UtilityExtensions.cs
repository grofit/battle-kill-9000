using System.Collections.Generic;
using System.Linq;
using BK9K.UAI.Keys;

namespace BK9K.UAI.Extensions
{
    public static class UtilityExtensions
    {
        public static float CalculateScore(this IReadOnlyCollection<KeyValuePair<UtilityKey, float>> variableUtilities)
        { return CalculateScore(variableUtilities.Select(x => x.Value).ToArray()); }

        public static float CalculateScore(this IReadOnlyCollection<float> utilities)
        {
            var score = 1.0f;
            var compensationFactor = (float)(1.0 - 1.0 / utilities.Count);
            foreach (var utility in utilities)
            {
                var modification = (float)((1.0 - utility) * compensationFactor);
                var scaledUtility = utility + (modification * utility);
                score *= scaledUtility;
            }
            return score;
        }
    }
}
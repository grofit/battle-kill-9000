using System.Collections.Generic;
using System.Linq;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Variables;

namespace BK9K.Game.AI.Extensions
{
    public static class IUtilityVariableExtensions
    {
        public static IEnumerable<KeyValuePair<int, float>> GetRelatedScoresFor(this IUtilityVariables variables, params int[] utilityIds)
        {
            var cache = new Dictionary<int, List<float>>();
            foreach (var utilityId in utilityIds)
            {
                var relatedUtilities = variables.GetRelatedUtilities(utilityId);
                foreach (var relatedUtility in relatedUtilities)
                {
                    if (cache.ContainsKey(relatedUtility.Key.RelatedId))
                    {
                        cache[relatedUtility.Key.RelatedId].Add(relatedUtility.Value);
                        continue;
                    }
                    
                    cache.Add(relatedUtility.Key.RelatedId, new List<float> { relatedUtility.Value });
                }
            }

            return cache.Select(x => new KeyValuePair<int,float>(x.Key, x.Value.Sum())).OrderByDescending(x => x.Value);
        }

        public static int GetBestRelatedIdFor(this IUtilityVariables variables, params int[] utilityIds)
        {
            var entries = GetRelatedScoresFor(variables, utilityIds).ToArray();
            if (entries.Length > 0) { return entries[0].Key; }
            return -1;
        }
    }
}
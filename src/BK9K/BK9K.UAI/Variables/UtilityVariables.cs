using System.Collections.Generic;
using System.Linq;
using BK9K.UAI.Keys;
using OpenRpg.Core.Variables;

namespace BK9K.UAI.Variables
{
    public class UtilityVariables : DefaultKeyedVariables<UtilityKey, float>, IUtilityVariables
    {
        public IReadOnlyCollection<KeyValuePair<UtilityKey, float>> GetRelatedUtilities(int utilityId)
        { return InternalVariables.Where(x => x.Key.UtilityId == utilityId).ToArray(); }

        public void RemoveVariable(int utilityId)
        { RemoveVariable(new UtilityKey(utilityId)); }

        public bool HasVariable(int utilityId)
        { return HasVariable(new UtilityKey(utilityId)); }

        public float this[int utilityId]
        {
            get => InternalVariables[new UtilityKey(utilityId)];
            set => this[new UtilityKey(utilityId)] = value;
        }
    }
}
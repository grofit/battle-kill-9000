using System.Collections.Generic;
using BK9K.UAI.Keys;
using OpenRpg.Core.Variables;

namespace BK9K.UAI.Variables
{
    public interface IUtilityVariables : IKeyedVariables<UtilityKey, float>
    {
        IReadOnlyCollection<KeyValuePair<UtilityKey, float>> GetRelatedUtilities(int utilityId);
        void RemoveVariable(int utilityId);
        bool ContainsKey(int utilityId);

        float this[int utilityId] { get; set; }
    }
}
using BK9K.UAI.Keys;
using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public interface IConsideration
    {
        UtilityKey UtilityId { get; }
        float CalculateUtility(IUtilityVariables utilityVariables);
    }
}
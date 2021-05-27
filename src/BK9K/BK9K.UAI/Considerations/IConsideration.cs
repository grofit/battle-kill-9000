using BK9K.UAI.Variables;

namespace BK9K.UAI.Considerations
{
    public interface IConsideration
    {
        float CalculateUtility(IUtilityVariables utilityVariables);
    }
}
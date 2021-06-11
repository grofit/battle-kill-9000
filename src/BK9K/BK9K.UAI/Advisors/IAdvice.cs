using System.Collections.Generic;
using BK9K.UAI.Keys;

namespace BK9K.UAI.Advisors
{
    public interface IAdvice
    {
        int AdviceId { get; }
        float UtilityValue { get; set; }
        IEnumerable<UtilityKey> UtilityKeys { get; }
        object GetRelatedContext();
    }
}
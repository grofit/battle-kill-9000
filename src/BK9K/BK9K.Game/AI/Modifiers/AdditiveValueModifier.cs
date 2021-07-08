using OpenRpg.AdviceEngine.Modifiers;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace BK9K.Game.AI.Modifiers
{
    public class AdditiveValueModifier : IValueModifier
    {
        public float AdditiveValue { get; }

        public AdditiveValueModifier(float additiveValue)
        {
            AdditiveValue = additiveValue;
        }

        public bool ShouldApply(IHasDataId ownerContext, IUtilityVariables utilityVariables) => true;

        public float ModifyValue(float currentValue, IHasDataId ownerContext, IUtilityVariables utilityVariables)
        { return currentValue + AdditiveValue; }
    }
}
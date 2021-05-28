using OpenRpg.Core.Common;

namespace BK9K.UAI.Accessors
{
    public interface IValueAccessor : IHasDataId
    {
        float GetValue();
        object GetRelatedContext();
    }
}
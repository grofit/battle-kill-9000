using System;
using EcsRx.MicroRx;

namespace BK9K.UAI.Accessors
{
    public class ManualValueAccessor : IValueAccessor
    {
        public int Id => 0;
        
        public Func<float> GetValueFunction { get; }
        public Func<object> GetRelatedContextFunction { get; }

        public ManualValueAccessor(Func<float> getValueFunction, Func<object> getRelatedContextFunction)
        {
            GetValueFunction = getValueFunction;
            GetRelatedContextFunction = getRelatedContextFunction;
        }

        public float GetValue() => GetValueFunction();
        public object GetRelatedContext() => GetRelatedContextFunction();
    }
}
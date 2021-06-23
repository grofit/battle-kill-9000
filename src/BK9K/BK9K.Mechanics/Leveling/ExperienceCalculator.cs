using BK9K.Mechanics.Levelling;
using OpenRpg.CurveFunctions;
using OpenRpg.CurveFunctions.Extensions;

namespace BK9K.Mechanics.Leveling
{
    public class ExperienceCalculator : IExperienceCalculator
    {
        public ICurveFunction CurveFunction { get; set; } = PresetCurves.Linear;
        public int ScalingFactor { get; set; } = 100;
        public int MaxExperienceValue { get; set; } = 100000;

        public int GetExperienceRequiredForLevel(int level)
        {
            if(level < 0) { return ScalingFactor; }
            return (int)CurveFunction.ScaledPlot(ScalingFactor * level, MaxExperienceValue);
        }
    }
}
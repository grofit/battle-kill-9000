using System.Numerics;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Handlers;
using BK9K.UAI.Tester.Variables.Extensions;
using BK9K.UAI.Tester.Variables.Types;
using MicroUnit = EcsRx.MicroRx.Unit;

namespace BK9K.UAI.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Have a variable
            var healthValue = 25; // Out of 100
            var targetA = Vector3.Zero;
            var targetB = Vector3.One * 2;
            var healthValueAccessor = new ManualValueAccessor(() => healthValue, () => null);
            var distanceValueAccessor = new ManualValueAccessor(() => Vector3.Distance(targetA, targetB), () => targetA);
            
            // Make an agent
            var considerationHandler = new ConsiderationHandler(new DefaultConsiderationScheduler());
            var agent = new Agent(considerationHandler);
           
            // Create considerations
            var needsHealingInput = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.InverseLinear);
            var needsHealingInput2 = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.QuadraticLowerLeft);
            var needsHealingInput3 = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.InverseLogit);
            var distanceToTarget = new ValueBasedConsideration(distanceValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.QuadraticLowerLeft);

            // Add considerations to agent
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing, needsHealingInput);
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing2, needsHealingInput2);
            agent.AddConsideration(UtilityVariableTypes.NeedsHealing3, needsHealingInput3);
            agent.AddConsideration(UtilityVariableTypes.TargetDistance, distanceToTarget);
            
            // Check results
            agent.UtilityVariables.PrintVariables();
        }
    }
}
using System;
using System.Linq;
using System.Numerics;
using BK9K.UAI.Accessors;
using BK9K.UAI.Clampers;
using BK9K.UAI.Considerations;
using BK9K.UAI.Evaluators;
using BK9K.UAI.Extensions;
using BK9K.UAI.Handlers;
using BK9K.UAI.Keys;
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
            var agent = new Agent(null, considerationHandler);
           
            // Create considerations
            var needsHealingInput = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.InverseLinear);
            var needsHealingInput2 = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.QuadraticLowerLeft);
            var needsHealingInput3 = new ValueBasedConsideration(healthValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.InverseLogit);
            var distanceToTarget = new ValueBasedConsideration(distanceValueAccessor, PresetClampers.ZeroToHundred, PresetEvaluators.QuadraticLowerLeft);

            // Add considerations to agent
            agent.AddConsideration(new UtilityKey(UtilityVariableTypes.NeedsHealing, 1), needsHealingInput);
            agent.AddConsideration(new UtilityKey(UtilityVariableTypes.NeedsHealing, 2), needsHealingInput2);
            agent.AddConsideration(new UtilityKey(UtilityVariableTypes.NeedsHealing, 3), needsHealingInput3);
            var relatedHealingVars = agent.ConsiderationHandler.UtilityVariables.GetRelatedUtilities(UtilityVariableTypes.NeedsHealing);
            var healingScore = relatedHealingVars.CalculateScore();
            var healingAverage = relatedHealingVars.Select(x => x.Value).Average();
            
            agent.AddConsideration(UtilityVariableTypes.TargetDistance, distanceToTarget);
            
            // Check results
            agent.UtilityVariables.PrintVariables();
            Console.WriteLine($"NeedsHealing:Score - {healingScore}");
            Console.WriteLine($"NeedsHealing:Average - {healingAverage}");
        }
    }
}
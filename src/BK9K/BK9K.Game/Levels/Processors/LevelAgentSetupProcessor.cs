using System.Threading.Tasks;
using BK9K.Game.AI;
using BK9K.Game.Processors;

namespace BK9K.Game.Levels.Processors
{
    public class LevelAgentSetupProcessor : IProcessor<Level>
    {
        public int Priority => 6;
        
        public ConsiderationGenerator ConsiderationGenerator { get; }
        public AdviceGenerator AdviceGenerator { get; }
        
        public Task Process(Level context)
        {
            ProcessAgentLocalConsiderations(context);
            ProcessAgentLevelConsiderations(context);
            ProcessAgentAdvice(context);
            return Task.CompletedTask;
        }

        public void ProcessAgentLocalConsiderations(Level level)
        {
            foreach (var gameUnit in level.GameUnits)
            {
                ConsiderationGenerator.PopulateLocalConsiderations(gameUnit.Agent);
            }
        }

        public void ProcessAgentLevelConsiderations(Level level)
        {
            foreach (var gameUnits in level.GameUnits)
            {
                ConsiderationGenerator.PopulateExternalConsiderations(gameUnits.Agent, level);
            }
        }

        public void ProcessAgentAdvice(Level level)
        {
            foreach (var gameUnits in level.GameUnits)
            {
                AdviceGenerator.PopulateAdvice(gameUnits.Agent, level);
            }
        }
    }
}
using System.Threading.Tasks;
using BK9K.Game.AI;
using BK9K.Game.Configuration;
using BK9K.Game.Extensions;
using BK9K.Game.Processors;

namespace BK9K.Game.Levels.Processors
{
    public class LevelPlayerUnitSetupProcessor : IProcessor<Level>
    {
        public int Priority => 3;
        
        public GameState GameState { get; }
        public AgentFactory AgentFactory { get; }

        public LevelPlayerUnitSetupProcessor(GameState gameState, AgentFactory agentFactory)
        {
            GameState = gameState;
            AgentFactory = agentFactory;
        }

        public Task Process(Level context)
        {
            var playerGameUnits = AgentFactory.GenerateGameUnits(GameState.PlayerUnits);
            context.GameUnits.AddRange(playerGameUnits);
            return Task.CompletedTask;
        }
    }
}
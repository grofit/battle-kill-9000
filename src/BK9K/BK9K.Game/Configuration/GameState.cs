using OpenRpg.Core.Variables;
using OpenRpg.Quests.States;

namespace BK9K.Game.Configuration
{
    public class GameState : IGameState
    {
        public int LevelId { get; set; } = 1;

        public IVariables<bool> Triggers { get; }
        public IVariables<int> QuestStates { get; }
    }
}
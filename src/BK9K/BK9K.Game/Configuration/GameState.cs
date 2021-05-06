using System.Collections.Generic;
using BK9K.Framework.Units;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.States;

namespace BK9K.Game.Configuration
{
    public class GameState : IGameState
    {
        public int LevelId { get; set; } = 1;
        public List<Unit> PlayerUnits { get; set; } = new List<Unit>();

        public IVariables<bool> Triggers { get; }
        public IVariables<int> QuestStates { get; }
    }
}
using System.Collections.Generic;
using BK9K.Mechanics.Units;
using OpenRpg.Cards;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.States;

namespace BK9K.Game.Configuration
{
    public class GameState : IGameState
    {
        public int LevelId { get; set; } = 1;
        public List<Unit> PlayerUnits { get; set; } = new();
        public List<ICard> PlayerCards { get; set; } = new();

        public IVariables<bool> Triggers { get; }
        public IVariables<int> QuestStates { get; }
    }
}
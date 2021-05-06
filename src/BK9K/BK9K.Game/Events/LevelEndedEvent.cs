namespace BK9K.Game.Events
{
    public class LevelEndedEvent
    {
        public int CurrentLevelId { get; set; }
        public int NextLevelId { get; set; }
        public bool DidPlayerWin { get; set; }

        public LevelEndedEvent(bool didPlayerWin, int currentLevelId, int nextLevelId)
        {
            DidPlayerWin = didPlayerWin;
            CurrentLevelId = currentLevelId;
            NextLevelId = nextLevelId;
        }
    }
}
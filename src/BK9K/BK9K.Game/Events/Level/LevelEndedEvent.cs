namespace BK9K.Game.Events.Level
{
    public class LevelEndedEvent
    {
        public int CurrentLevelId { get; set; }
        public bool DidPlayerWin { get; set; }

        public LevelEndedEvent(bool didPlayerWin, int currentLevelId)
        {
            DidPlayerWin = didPlayerWin;
            CurrentLevelId = currentLevelId;
        }
    }
}
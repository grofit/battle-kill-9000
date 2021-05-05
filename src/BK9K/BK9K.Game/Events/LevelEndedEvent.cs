namespace BK9K.Game.Events
{
    public class LevelEndedEvent
    {
        public bool DidPlayerWin { get; set; }

        public LevelEndedEvent(bool didPlayerWin)
        {
            DidPlayerWin = didPlayerWin;
        }
    }
}
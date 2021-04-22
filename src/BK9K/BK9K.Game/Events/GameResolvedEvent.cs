namespace BK9K.Game.Events
{
    public class GameResolvedEvent
    {
        public bool DidPlayerWin { get; set; }

        public GameResolvedEvent(bool didPlayerWin)
        {
            DidPlayerWin = didPlayerWin;
        }
    }
}
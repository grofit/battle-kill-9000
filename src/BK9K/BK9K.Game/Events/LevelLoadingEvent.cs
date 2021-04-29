namespace BK9K.Game.Events
{
    public class LevelLoadingEvent
    {
        public int Level { get; }

        public LevelLoadingEvent(int level)
        {
            Level = level;
        }
    }
}
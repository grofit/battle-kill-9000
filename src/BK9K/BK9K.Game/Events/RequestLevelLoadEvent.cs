namespace BK9K.Game.Events
{
    public class RequestLevelLoadEvent
    {
        public int Level { get; }

        public RequestLevelLoadEvent(int level)
        {
            Level = level;
        }
    }
}
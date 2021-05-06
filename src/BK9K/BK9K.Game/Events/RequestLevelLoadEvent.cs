namespace BK9K.Game.Events
{
    public class RequestLevelLoadEvent
    {
        public int LevelId { get; }

        public RequestLevelLoadEvent(int levelId)
        {
            LevelId = levelId;
        }
    }
}
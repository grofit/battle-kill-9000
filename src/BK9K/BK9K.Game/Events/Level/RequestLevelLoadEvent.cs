namespace BK9K.Game.Events.Level
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
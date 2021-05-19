using BK9K.Framework.Cards;
using BK9K.Framework.Transforms;

namespace BK9K.Game.Events.Cards
{
    public class CardUsedOnTileEvent
    {
        public ICard Card { get; }
        public Position Position { get; }

        public CardUsedOnTileEvent(ICard card, Position position)
        {
            Card = card;
            Position = position;
        }
    }
}
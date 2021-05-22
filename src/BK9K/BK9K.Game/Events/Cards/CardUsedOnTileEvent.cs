using System.Numerics;
using BK9K.Cards;

namespace BK9K.Game.Events.Cards
{
    public class CardUsedOnTileEvent
    {
        public ICard Card { get; }
        public Vector2 Position { get; }

        public CardUsedOnTileEvent(ICard card, Vector2 position)
        {
            Card = card;
            Position = position;
        }
    }
}
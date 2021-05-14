using BK9K.Framework.Cards;

namespace BK9K.Game.Events.Cards
{
    public class PlayerCardRemovedEvent
    {
        public ICard Card { get; set; }

        public PlayerCardRemovedEvent(ICard card)
        {
            Card = card;
        }
    }
}
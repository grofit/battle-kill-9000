using BK9K.Framework.Cards;
using BK9K.Framework.Units;

namespace BK9K.Game.Events.Cards
{
    public class CardUsedOnUnitEvent
    {
        public ICard Card { get; set; }
        public Unit Unit { get; set; }

        public CardUsedOnUnitEvent(ICard card, Unit unit)
        {
            Card = card;
            Unit = unit;
        }
    }
}
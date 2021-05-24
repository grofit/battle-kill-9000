using BK9K.Mechanics.Units;
using OpenRpg.Cards;

namespace BK9K.Game.Events.Cards
{
    public class CardUsedOnUnitEvent
    {
        public ICard Card { get; }
        public Unit Unit { get; }

        public CardUsedOnUnitEvent(ICard card, Unit unit)
        {
            Card = card;
            Unit = unit;
        }
    }
}
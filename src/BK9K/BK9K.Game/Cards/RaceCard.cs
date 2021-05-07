using BK9K.Game.Cards.Conventions;
using BK9K.Game.Types;
using OpenRpg.Core.Races;

namespace BK9K.Game.Cards
{
    public class RaceCard : GenericDataCardWithEffects<IRaceTemplate>
    {
        public override int CardType => CardTypes.RaceCard;

        public RaceCard(IRaceTemplate data) : base(data) {}
    }
}
using BK9K.Cards.Genres.Conventions;
using BK9K.Cards.Types;
using OpenRpg.Core.Races;

namespace BK9K.Cards.Genres
{
    public class RaceCard : GenericDataCardWithEffects<IRaceTemplate>
    {
        public override int CardType => CardTypes.RaceCard;

        public RaceCard(IRaceTemplate data) : base(data) {}
    }
}
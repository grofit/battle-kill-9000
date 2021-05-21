using BK9K.Cards.Effects;
using BK9K.Cards.Genres.Conventions;
using BK9K.Cards.Types;

namespace BK9K.Cards.Genres
{
    public class EffectCard : GenericDataCardWithEffects<CardEffects>
    {
        public override int CardType => CardTypes.EffectCard;

        public EffectCard(CardEffects data) : base(data)
        {
        }
    }
}
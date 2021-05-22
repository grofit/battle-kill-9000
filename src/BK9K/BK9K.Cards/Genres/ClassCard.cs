using BK9K.Cards.Genres.Conventions;
using BK9K.Cards.Types;
using OpenRpg.Core.Classes;

namespace BK9K.Cards.Genres
{
    public class ClassCard : GenericDataCardWithEffects<IClassTemplate>
    {
        public override int CardType => CardTypes.ClassCard;

        public ClassCard(IClassTemplate data) : base(data) { }
    }
}
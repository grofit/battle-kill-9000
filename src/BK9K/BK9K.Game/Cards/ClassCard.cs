using BK9K.Game.Cards.Conventions;
using BK9K.Game.Types;
using OpenRpg.Core.Classes;

namespace BK9K.Game.Cards
{
    public class ClassCard : GenericDataCardWithEffects<IClassTemplate>
    {
        public override int CardType => CardTypes.ClassCard;

        public ClassCard(IClassTemplate data) : base(data) { }
    }
}
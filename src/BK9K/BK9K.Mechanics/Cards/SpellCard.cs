using System.Collections.Generic;
using BK9K.Mechanics.Spells;
using BK9K.Mechanics.Types;
using OpenRpg.Cards.Genres.Conventions;
using OpenRpg.Core.Effects;

namespace BK9K.Mechanics.Cards
{
    public class SpellCard : GenericDataCard<Spell>
    {
        public override int CardType => CardTypes.SpellCard;
        public override IEnumerable<Effect> Effects { get; } = new Effect[0];

        public SpellCard(Spell spell) : base(spell)
        {}
    }
}
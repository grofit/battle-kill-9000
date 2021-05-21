using System;
using System.Collections.Generic;
using BK9K.Cards.Types;
using BK9K.Mechanics.Spells;
using OpenRpg.Core.Effects;

namespace BK9K.Cards.Genres
{
    public class SpellCard : ICard
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public int CardType => CardTypes.SpellCard;
        public Spell Data { get; }

        public string NameLocaleId => Data.NameLocaleId;
        public string DescriptionLocaleId => Data.DescriptionLocaleId;
        public IEnumerable<Effect> Effects { get; } = new Effect[0];

        public SpellCard(Spell spell)
        {
            Data = spell;
        }
    }
}
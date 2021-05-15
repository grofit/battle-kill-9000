using System;
using System.Collections.Generic;
using BK9K.Framework.Abilities;
using BK9K.Framework.Cards;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;

namespace BK9K.Game.Cards
{
    public class AbilityCard : ICard
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public int CardType => CardTypes.AbilityCard;
        public Ability Data { get; }

        public string NameLocaleId => Data.NameLocaleId;
        public string DescriptionLocaleId => Data.DescriptionLocaleId;
        public IEnumerable<Effect> Effects { get; } = new Effect[0];

        public AbilityCard(Ability ability)
        {
            Data = ability;
        }
    }
}
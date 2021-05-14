using System;
using System.Collections.Generic;
using BK9K.Framework.Cards;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;

namespace BK9K.Game.Cards
{
    public class EffectCard : ICard
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public int CardType => CardTypes.EffectCard;

        public string NameLocaleId { get; }
        public string DescriptionLocaleId { get; }
        public IEnumerable<Effect> Effects { get; }

        public EffectCard(string nameLocaleId, string descriptionLocaleId, IEnumerable<Effect> effects)
        {
            NameLocaleId = nameLocaleId;
            DescriptionLocaleId = descriptionLocaleId;
            Effects = effects;
        }
    }
}
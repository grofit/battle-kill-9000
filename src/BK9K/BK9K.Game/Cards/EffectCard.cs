using System;
using System.Collections.Generic;
using BK9K.Framework.Cards;
using BK9K.Framework.Conventions;
using BK9K.Framework.Effects;
using BK9K.Game.Cards.Conventions;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;

namespace BK9K.Game.Cards
{
    public class EffectCard : GenericDataCardWithEffects<NamedEffects>, IHasUniqueId
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public override int CardType => CardTypes.EffectCard;

        public EffectCard(NamedEffects data) : base(data)
        {
        }
    }
}
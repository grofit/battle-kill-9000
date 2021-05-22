using System;
using System.Collections.Generic;
using BK9K.Cards.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Items;

namespace BK9K.Cards.Genres
{
    public class EquipmentCard : ICard
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int CardType => CardTypes.EquipmentCard;
        public IItem Item { get; }

        public EquipmentCard(IItem item)
        { Item = item; }

        public string NameLocaleId => Item.ItemTemplate.NameLocaleId;
        public string DescriptionLocaleId => Item.ItemTemplate.DescriptionLocaleId;
        public IEnumerable<Effect> Effects => Item.GetItemEffects();
    }
}
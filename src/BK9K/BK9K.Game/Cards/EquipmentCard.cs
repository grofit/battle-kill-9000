﻿using System.Collections.Generic;
using BK9K.Framework.Cards;
using BK9K.Game.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Items;

namespace BK9K.Game.Cards
{
    public class EquipmentCard : ICard
    {
        public int CardType => CardTypes.EquipmentCard;
        public IItem Item { get; }

        public EquipmentCard(IItem item)
        { Item = item; }

        public string NameLocaleId => Item.ItemTemplate.NameLocaleId;
        public string DescriptionLocaleId => Item.ItemTemplate.DescriptionLocaleId;
        public IEnumerable<Effect> Effects => Item.GetItemEffects();
    }
}
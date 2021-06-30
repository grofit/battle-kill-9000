using System;
using System.Collections.Generic;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Spells;
using OpenRpg.Cards.Effects;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Requirements;
using OpenRpg.Items;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;

namespace BK9K.Mechanics.Loot
{
    public class CustomLootTableEntry : ICustomLootTableEntry
    {
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public ILootTableEntryVariables Variables { get; set; } = new DefaultLootTableEntryVariables();
        public IItem Item { get; set; } = null;
        public UnitAbility Ability { get; set; } = null;
        public Spell Spell { get; set; } = null;
        public IClassTemplate Class { get; set; } = null;
        public CardEffects CardEffects { get; set; } = null;
    }
}
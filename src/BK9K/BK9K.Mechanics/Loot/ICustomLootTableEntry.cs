using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Spells;
using OpenRpg.Cards.Effects;
using OpenRpg.Core.Classes;
using OpenRpg.Items.Loot;

namespace BK9K.Mechanics.Loot
{
    public interface ICustomLootTableEntry : ILootTableEntry
    {
        UnitAbility Ability { get; }
        Spell Spell { get; }
        IClassTemplate Class { get; }
        CardEffects CardEffects { get; set; }
    }
}
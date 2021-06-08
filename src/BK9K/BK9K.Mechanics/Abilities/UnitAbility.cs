using OpenRpg.Combat.Abilities;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Mechanics.Abilities
{
    public class UnitAbility : Ability
    {
        public int Size { get; set; } = 1;
        public int Range { get; set; } = 1;
        public int DamageType { get; set; } = DamageTypes.SlashingDamage;
    }
}
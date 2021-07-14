using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Modifiers;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Mechanics.Combat.Modifiers
{
    public class RemoveLightDamageModifier : IAttackModifier
    {
        public bool ShouldApply(Attack attack) => true;

        public Attack ModifyValue(Attack attack)
        {
            attack.Damages = attack.Damages.Where(x => x.Type != DamageTypes.LightDamage).ToArray();
            return attack;
        }
    }
}
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Combat;
using System.Linq;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Mechanics.Combat
{
    public class CustomAttackGenerator : BasicAttackGenerator, IAttackGenerator
    {
        public CustomAttackGenerator(IRandomizer randomizer) : base(randomizer)
        {}

        public new Attack GenerateAttack(IStatsVariables stats)
        {
            var attack = base.GenerateAttack(stats);
            attack.Damages = attack.Damages.Where(x => x.Type != DamageTypes.LightDamage).ToArray();
            return attack;
        }
    }
}
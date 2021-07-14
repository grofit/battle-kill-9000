using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Combat;
using BK9K.Mechanics.Combat.Modifiers;
using OpenRpg.Combat.Processors.Modifiers;

namespace BK9K.Mechanics.Combat
{
    public class CustomAttackGenerator : BasicAttackGenerator, IAttackGenerator
    {
        public IAttackModifier RemoveLightDamageModifier { get; } = new RemoveLightDamageModifier();

        public new Attack GenerateAttack(IStatsVariables stats)
        {
            var attack = base.GenerateAttack(stats);
            return RemoveLightDamageModifier.ModifyValue(attack);
        }
    }
}
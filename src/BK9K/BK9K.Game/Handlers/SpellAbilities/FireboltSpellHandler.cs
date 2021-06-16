using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Spells;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;

namespace BK9K.Game.Handlers.SpellAbilities
{
    public class FireboltSpellHandler : ISpellHandler
    {
        public int Id => SpellLookups.Firebolt;

        public Level Level { get; }
        public IAttackProcessor AttackProcessor { get; }
        public IAttackGenerator AttackGenerator { get; }
        public IStatsComputer StatsComputer { get; }

        public FireboltSpellHandler(Level level, IAttackProcessor attackProcessor, IStatsComputer statsComputer, IAttackGenerator attackGenerator)
        {
            Level = level;
            AttackProcessor = attackProcessor;
            StatsComputer = statsComputer;
            AttackGenerator = attackGenerator;
        }

        public async Task<bool> ExecuteSpell(Spell spell, Vector2 target)
        {
            var unit = Level.GetUnitAt(target);
            if (unit == null) { return false; }

            var stats = StatsComputer.ComputeStats(spell.Effects.ToArray());
            var attack = AttackGenerator.GenerateAttack(stats);
            var processedAttack = AttackProcessor.ProcessAttack(attack, unit.Unit.Stats);
            unit.Unit.ApplyDamageToTarget(processedAttack);
            return true;
        }
    }
}
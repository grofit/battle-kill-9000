using System.Linq;
using System.Threading.Tasks;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Framework.Spells;
using BK9K.Framework.Transforms;
using BK9K.Game.Extensions;
using BK9K.Game.Types;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;

namespace BK9K.Game.Handlers.SpellAbilities
{
    public class FireboltSpellHandler : ISpellHandler
    {
        public int Id => SpellTypes.Firebolt;

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
        
        public async Task<bool> ExecuteSpell(Spell spell, Position target)
        {
            var unit = Level.GetUnitAt(target);
            if(unit == null) { return false; }

            var stats = StatsComputer.ComputeStats(spell.Effects.ToArray());
            var attack = AttackGenerator.GenerateAttack(stats);
            var processedAttack = AttackProcessor.ProcessAttack(attack, unit.Stats);
            unit.ApplyDamageToTarget(processedAttack);
            return true;
        }
    }
}
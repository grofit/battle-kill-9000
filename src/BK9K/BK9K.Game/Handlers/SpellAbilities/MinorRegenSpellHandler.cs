using System.Linq;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Framework.Extensions;
using BK9K.Framework.Levels;
using BK9K.Framework.Spells;
using BK9K.Framework.Transforms;
using BK9K.Game.Events.Effects;
using BK9K.Game.Extensions;
using BK9K.Game.Types;
using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;

namespace BK9K.Game.Handlers.SpellAbilities
{
    public class MinorRegenSpellHandler : ISpellHandler
    {
        public int Id => SpellTypes.MinorRegen;

        public Level Level { get; }
        public IEventSystem EventSystem { get; }

        public MinorRegenSpellHandler(Level level, IEventSystem eventSystem)
        {
            Level = level;
            EventSystem = eventSystem;
        }

        public async Task<bool> ExecuteSpell(Spell spell, Position target)
        {
            var unit = Level.GetUnitAt(target);
            if(unit == null) { return false; }

            var existingRegen = unit.ActiveEffects.SingleOrDefault(x => x.Effect.Id == TimedEffectTypes.MinorRegen);
            if (existingRegen != null)
            {
                if (existingRegen.Stacks < existingRegen.Effect.MaxStack)
                {
                    existingRegen.Stacks++;
                    existingRegen.ActiveTime = 0;
                    return true;
                }
                return false;
            }

            var activeRegenEffect = new ActiveEffect
            {
                Effect = spell.Effects.First() as TimedEffect,
                Stacks = 1
            };
            unit.ActiveEffects.Add(activeRegenEffect);

            EventSystem.Publish(new EffectAddedEvent { ActiveEffect = activeRegenEffect, Unit = unit });
            return true;
        }
    }
}
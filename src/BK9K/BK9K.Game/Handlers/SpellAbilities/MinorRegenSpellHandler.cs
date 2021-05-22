using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SystemsRx.Events;
using BK9K.Game.Events.Effects;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Handlers;
using BK9K.Mechanics.Levels;
using BK9K.Mechanics.Spells;
using BK9K.Mechanics.Types;
using OpenRpg.Combat.Effects;

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

        public async Task<bool> ExecuteSpell(Spell spell, Vector2 target)
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
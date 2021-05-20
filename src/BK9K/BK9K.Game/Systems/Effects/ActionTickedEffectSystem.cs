using System;
using System.Collections.Generic;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Events.Effects;
using BK9K.Game.Events.Units;
using BK9K.Game.Extensions;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Systems.Effects
{
    public class ActionTickedEffectSystem : IReactToEventSystem<EffectTickedEvent>
    {
        public IEventSystem EventSystem { get; }
        public IAttackProcessor AttackProcessor { get; }

        public ActionTickedEffectSystem(IEventSystem eventSystem, IAttackProcessor attackProcessor)
        {
            EventSystem = eventSystem;
            AttackProcessor = attackProcessor;
        }

        public void Process(EffectTickedEvent eventData)
        {
            if (eventData.ActiveEffect.Effect.IsDamagingEffect())
            {
                if (eventData.ActiveEffect.Effect.EffectType == EffectTypes.LightBonusAmount)
                { ApplyHealingEffect(eventData); }
                else
                { ApplyDamagingEffect(eventData); }
            }

            // I dont think we will have any other types
            return;
        }

        public void ApplyDamagingEffect(EffectTickedEvent eventData)
        {
            var damage = new Damage
            {
                Type = eventData.ActiveEffect.Effect.GetApplicableDamageType(),
                Value = eventData.ActiveEffect.GetStackedPotency()
            };

            var attack = new Attack(new List<Damage>() {damage});
            var processedAttack = AttackProcessor.ProcessAttack(attack, eventData.Unit.Stats);
            eventData.Unit.ApplyDamageToTarget(processedAttack);
            EventSystem.Publish(new UnitAttackedEvent(eventData.Unit, eventData.Unit, processedAttack));
        }
        
        public void ApplyHealingEffect(EffectTickedEvent eventData)
        {
            var healAmount = (int)Math.Round(eventData.ActiveEffect.GetStackedPotency());
            eventData.Unit.ApplyHealingToTarget(healAmount);

            var defense = new Damage
            {
                Type = eventData.ActiveEffect.Effect.GetApplicableDamageType(),
                Value = eventData.Unit.Stats.GetDefenseFor(eventData.ActiveEffect.Effect.EffectType)
            };

            var damage = new Damage
            {
                Type = defense.Type,
                Value = eventData.ActiveEffect.GetStackedPotency() + defense.Value
            };

            var processedAttack = new ProcessedAttack(new List<Damage> { damage }, new List<Damage>() { defense });

            EventSystem.Publish(new UnitAttackedEvent(eventData.Unit, eventData.Unit, processedAttack));
        }
    }
}
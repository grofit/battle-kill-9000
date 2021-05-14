﻿using System.Linq;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Extensions;
using BK9K.Framework.Units;
using BK9K.Game.Cards;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Systems
{
    public class ApplyCardToUnitSystem : IReactToEventSystem<CardUsedOnUnitEvent>
    {
        private IStatsComputer StatComputer { get; }
        private IEventSystem EventSystem { get; }
        private GameState GameState { get; }

        public ApplyCardToUnitSystem(IStatsComputer statComputer, GameState gameState, IEventSystem eventSystem)
        {
            StatComputer = statComputer;
            GameState = gameState;
            EventSystem = eventSystem;
        }

        public void Process(CardUsedOnUnitEvent eventData)
        {
            if (eventData.Card is ItemCard itemCard)
            { ApplyItemCardToUnit(itemCard, eventData.Unit); }
            else if(eventData.Card is EffectCard effectCard)
            { ApplyEffectCardToUnit(effectCard, eventData.Unit); }
            else { return; }

            GameState.PlayerCards.Remove(eventData.Card);
            EventSystem.Publish(new PlayerCardRemovedEvent(eventData.Card));
        }

        public void ApplyItemCardToUnit(ItemCard card, Unit unit)
        {
            foreach (var effect in card.Effects)
            {
                if (effect.EffectType == EffectTypes.HealthRestoreAmount)
                { unit.Stats.Health(unit.Stats.Health() + (int)effect.Potency); }
                else if (effect.EffectType == EffectTypes.MagicRestoreAmount)
                { unit.Stats.Magic(unit.Stats.Magic() + (int)effect.Potency); }
            }
        }

        public void ApplyEffectCardToUnit(EffectCard card, Unit unit)
        {
            var cardEffects = card.Effects;
            unit.CardEffects.AddRange(cardEffects);
            RefreshUnitStats(unit);
        }

        public void RefreshUnitStats(Unit unit)
        {
            var health = unit.Stats.Health();
            var magic = unit.Stats.Magic();

            var newStats = StatComputer.ComputeStats(unit.GetUnitEffects().ToArray());
            newStats.Health(health);
            newStats.Magic(magic);

            unit.Stats = newStats;
        }
    }
}
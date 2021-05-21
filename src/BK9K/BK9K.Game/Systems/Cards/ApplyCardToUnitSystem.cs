using System.Linq;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Cards.Genres;
using BK9K.Framework.Extensions;
using BK9K.Framework.Units;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace BK9K.Game.Systems.Cards
{
    public class ApplyCardToUnitSystem : IReactToEventSystem<CardUsedOnUnitEvent>
    {
        private IStatsComputer StatsComputer { get; }
        private IEventSystem EventSystem { get; }
        private GameState GameState { get; }

        public ApplyCardToUnitSystem(IStatsComputer statsComputer, GameState gameState, IEventSystem eventSystem)
        {
            StatsComputer = statsComputer;
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
            EventSystem.Publish(new PlayerCardsChangedEvent());
        }

        public void ApplyItemCardToUnit(ItemCard card, Unit unit)
        {
            foreach (var effect in card.Effects)
            {
                if (effect.EffectType == EffectTypes.HealthRestoreAmount)
                { unit.Stats.AddHealth((int)effect.Potency); }
                else if (effect.EffectType == EffectTypes.MagicRestoreAmount)
                { unit.Stats.AddMagic((int)effect.Potency); }
            }
        }

        public void ApplyEffectCardToUnit(EffectCard card, Unit unit)
        {
            var cardEffects = card.Effects;
            unit.PassiveEffects.AddRange(cardEffects);
            RefreshUnitStats(unit);
        }

        public void RefreshUnitStats(Unit unit)
        {
            var health = unit.Stats.Health();
            var magic = unit.Stats.Magic();

            StatsComputer.RecomputeStats(unit.Stats, unit.GetUnitEffects().ToArray());
            unit.Stats.Health(health);
            unit.Stats.Magic(magic);
        }
    }
}
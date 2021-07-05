using System.Linq;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.AI.Service;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using BK9K.Game.Levels;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Units;
using OpenRpg.Cards.Genres;
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
        private Level Level { get; }
        private IAgentService AgentService { get; }

        public ApplyCardToUnitSystem(IStatsComputer statsComputer, GameState gameState, IEventSystem eventSystem, IAgentService agentService, Level level)
        {
            StatsComputer = statsComputer;
            GameState = gameState;
            EventSystem = eventSystem;
            AgentService = agentService;
            Level = level;
        }

        public void Process(CardUsedOnUnitEvent eventData)
        {
            if (eventData.Card is ItemCard itemCard)
            { ApplyItemCardToUnit(itemCard, eventData.Unit); }
            else if(eventData.Card is EffectCard effectCard)
            { ApplyEffectCardToUnit(effectCard, eventData.Unit); }
            else if (eventData.Card is EquipmentCard equipmentCard)
            { ApplyEquipmentCardToUnit(equipmentCard, eventData.Unit); }
            else if (eventData.Card is AbilityCard abilityCard)
            { ApplyAbilityCardToUnit(abilityCard, eventData.Unit); }
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
            unit.AddOrApplyPassiveEffects(card.Effects);
            RefreshUnitStats(unit);
        }

        public void ApplyEquipmentCardToUnit(EquipmentCard card, Unit unit)
        {
            var item = card.Data;
            var previousEquipment = unit.EquipItem(item);
            if (previousEquipment != null)
            { GameState.PlayerCards.Add(new EquipmentCard(previousEquipment)); }
            RefreshUnitStats(unit);
        }

        public void ApplyAbilityCardToUnit(AbilityCard card, Unit unit)
        {
            var ability = (UnitAbility)card.Data;
            var removedAbility = unit.EquipAbility(ability);
            if (removedAbility != null)
            { GameState.PlayerCards.Add(new AbilityCard(removedAbility)); }

            var gameUnit = Level.GameUnits.Single(x => x.Agent.OwnerContext.Id == unit.Id);
            AgentService.RefreshAgent(gameUnit.Agent);
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
﻿using System.Threading.Tasks;
using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Transforms;
using BK9K.Game.Cards;
using BK9K.Game.Configuration;
using BK9K.Game.Data;
using BK9K.Game.Events.Cards;
using OpenRpg.Core.Stats;

namespace BK9K.Game.Systems
{
    public class ApplyCardToTileSystem : IReactToEventSystem<CardUsedOnTileEvent>
    {
        private ISpellHandlerRepository SpellHandlerRepository { get; }
        private IEventSystem EventSystem { get; }
        private GameState GameState { get; }

        public ApplyCardToTileSystem(ISpellHandlerRepository spellHandlerRepository, IEventSystem eventSystem, GameState gameState)
        {
            SpellHandlerRepository = spellHandlerRepository;
            EventSystem = eventSystem;
            GameState = gameState;
        }

        public void Process(CardUsedOnTileEvent eventData)
        {
            var appliedCard = true;
            if (eventData.Card is SpellCard spellCard)
            {
                var task = ApplySpellCardToTile(spellCard, eventData.Position);
                task.Wait();
                if (task.Exception != null) { throw task.Exception; }
                appliedCard = task.Result;
            }
            else { appliedCard = false; }

            if(!appliedCard)
            { return; }

            GameState.PlayerCards.Remove(eventData.Card);
            EventSystem.Publish(new PlayerCardsChangedEvent());
        }

        public async Task<bool> ApplySpellCardToTile(SpellCard card, Position position)
        {
            var spellHandler = SpellHandlerRepository.Retrieve(card.Data.Id);
            return await spellHandler.ExecuteSpell(card.Data, position);
        }
    }
}
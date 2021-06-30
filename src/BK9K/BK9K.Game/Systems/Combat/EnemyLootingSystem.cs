using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using BK9K.Game.Events.Units;
using BK9K.Mechanics.Cards;
using BK9K.Mechanics.Loot;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.Cards;
using OpenRpg.Cards.Genres;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;

namespace BK9K.Game.Systems.Combat
{
    public class EnemyLootingSystem : IReactToEventSystem<UnitHasDiedEvent>
    {
        public GameState GameState { get; }
        public IEventSystem EventSystem { get; }

        public EnemyLootingSystem(GameState gameState, IEventSystem eventSystem)
        {
            GameState = gameState;
            EventSystem = eventSystem;
        }
        
        public void Process(UnitHasDiedEvent eventData)
        {
            var possibleEnemy = eventData.Target;
            var isADeadEnemy = possibleEnemy.FactionType == FactionTypes.Enemy;
            if (!isADeadEnemy) { return; }

            var enemyUnit = possibleEnemy as EnemyUnit;
            var enemyLoot = enemyUnit.LootTable.GetRandomLootEntries();

            var hasLoot = false;
            foreach (var loot in enemyLoot)
            {
                hasLoot = true;
                var cardLoot = GetCardForLoot(loot as ICustomLootTableEntry);
                GameState.PlayerCards.Add(cardLoot);
            }

            if(hasLoot)
            { EventSystem.Publish(new PlayerCardsChangedEvent()); }
        }

        public ICard GetCardForLoot(ICustomLootTableEntry lootEntry)
        {
            if (lootEntry.Item != null) { return new ItemCard((lootEntry.Item as DefaultItem).Clone()); }
            if (lootEntry.Ability != null) { return new AbilityCard(lootEntry.Ability); }
            if (lootEntry.Spell != null) { return new SpellCard(lootEntry.Spell); }
            if (lootEntry.Class != null) { return new ClassCard(lootEntry.Class); }
            if (lootEntry.CardEffects != null) { return new EffectCard(lootEntry.CardEffects); }
            return null;
        }
    }
}
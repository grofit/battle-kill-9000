using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using BK9K.Game.Events.Units;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.Cards.Genres;

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
            var enemyLoot = enemyUnit.LootTable.GetLoot();

            var hasLoot = false;
            foreach (var item in enemyLoot)
            {
                hasLoot = true;
                var itemCard = new ItemCard(item);
                GameState.PlayerCards.Add(itemCard);
            }

            if(hasLoot)
            { EventSystem.Publish(new PlayerCardsChangedEvent()); }
        }
    }
}
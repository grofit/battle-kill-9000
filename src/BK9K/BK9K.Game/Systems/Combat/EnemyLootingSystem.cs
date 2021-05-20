using SystemsRx.Events;
using SystemsRx.Systems.Conventional;
using BK9K.Framework.Units;
using BK9K.Game.Cards;
using BK9K.Game.Configuration;
using BK9K.Game.Events.Cards;
using BK9K.Game.Events.Units;
using BK9K.Game.Types;
using OpenRpg.Genres.Fantasy.Extensions;

namespace BK9K.Game.Systems.Combat
{
    public class EnemyLootingSystem : IReactToEventSystem<UnitAttackedEvent>
    {
        public GameState GameState { get; }
        public IEventSystem EventSystem { get; }

        public EnemyLootingSystem(GameState gameState, IEventSystem eventSystem)
        {
            GameState = gameState;
            EventSystem = eventSystem;
        }
        
        public void Process(UnitAttackedEvent eventData)
        {
            var possibleEnemy = eventData.Target;
            var isADeadEnemy = possibleEnemy.FactionType == FactionTypes.Enemy && possibleEnemy.Stats.Health() <= 0;
            if (!isADeadEnemy) { return; }

            var enemyUnit = possibleEnemy as EnemyUnit;
            var enemyLoot = enemyUnit.LootTable.GetLoot();

            foreach (var item in enemyLoot)
            {
                var itemCard = new ItemCard(item);
                GameState.PlayerCards.Add(itemCard);
            }

            EventSystem.Publish(new PlayerCardsChangedEvent());
        }
    }
}
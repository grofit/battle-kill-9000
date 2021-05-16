﻿using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Items.Loot;

namespace BK9K.Framework.Units
{
    public class EnemyUnit : Unit, IEnemy
    {
        public ILootTable LootTable { get; set;  }
    }
}
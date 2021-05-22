﻿using BK9K.Mechanics.Abilities;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data.Repositories
{
    public interface IAbilityRepository : IReadRepository<Ability, int>
    {
    }
}
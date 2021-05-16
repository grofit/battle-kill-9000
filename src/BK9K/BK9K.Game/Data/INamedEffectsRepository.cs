﻿using BK9K.Framework.Effects;
using OpenRpg.Data.Repositories;

namespace BK9K.Game.Data
{
    public interface INamedEffectsRepository : IReadRepository<NamedEffects, int>
    {
    }
}
using System;
using BK9K.Framework.Transforms;
using BK9K.Game.Conventions;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Framework.Units
{
    public class Unit : DefaultCharacter, IHasUniqueId
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int FactionType { get; set; }
        public int ActiveAbility { get; set; }
        public Position Position { get;set; }
    }
}
using System;
using System.Collections.Generic;
using BK9K.Framework.Conventions;
using BK9K.Framework.Transforms;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Framework.Units
{
    public class Unit : DefaultCharacter, IHasUniqueId
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int FactionType { get; set; }
        public int ActiveAbility { get; set; }
        public Position Position { get; set; }

        public List<Effect> CardEffects { get; set; } = new ();
    }
}
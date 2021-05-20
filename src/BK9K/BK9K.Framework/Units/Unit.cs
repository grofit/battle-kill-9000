using System;
using System.Collections.Generic;
using BK9K.Framework.Abilities;
using BK9K.Framework.Conventions;
using BK9K.Framework.Transforms;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Framework.Units
{
    public class Unit : DefaultCharacter, IHasUniqueId, IHasActiveEffects
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int FactionType { get; set; }
        public Ability ActiveAbility { get; set; }
        public Position Position { get; set; }

        public List<Effect> PassiveEffects { get; set; } = new ();
        public IList<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
    }
}
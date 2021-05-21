using System;
using System.Collections.Generic;
using System.Numerics;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Conventions;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Mechanics.Units
{
    public class Unit : DefaultCharacter, IHasUniqueId, IHasActiveEffects
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public int FactionType { get; set; }
        public Ability ActiveAbility { get; set; }
        public Vector2 Position { get; set; }

        public List<Effect> PassiveEffects { get; set; } = new ();
        public IList<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
    }
}
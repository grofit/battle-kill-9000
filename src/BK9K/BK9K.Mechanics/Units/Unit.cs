using System;
using System.Collections.Generic;
using System.Numerics;
using BK9K.Mechanics.Extensions;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Mechanics.Units
{
    public class Unit : DefaultCharacter, IHasActiveEffects
    {
        public int FactionType { get; set; }
        public Ability ActiveAbility { get; set; }
        public Vector2 Position { get; set; }

        public List<Effect> PassiveEffects { get; set; } = new ();
        public IList<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
    }
}
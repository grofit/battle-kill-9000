using System.Collections.Generic;
using System.Numerics;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Types;
using OpenRpg.Combat.Effects;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Mechanics.Units
{
    public class Unit : DefaultCharacter, IHasActiveEffects
    {
        public int FactionType { get; set; }
        public IList<UnitAbility> ActiveAbilities { get; set; } = new List<UnitAbility>();
        public Vector2 Position { get; set; }
        public int MovementRange { get; set; }
        public int FacingDirection { get; set; } = DirectionTypes.Up;

        public List<Effect> PassiveEffects { get; set; } = new ();
        public IList<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
    }
}
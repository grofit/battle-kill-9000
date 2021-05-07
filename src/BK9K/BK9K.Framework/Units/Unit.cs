using BK9K.Framework.Transforms;
using OpenRpg.Genres.Fantasy.Defaults;

namespace BK9K.Framework.Units
{
    public class Unit : DefaultCharacter
    {
        public int FactionType { get; set; }
        public int ActiveAbility { get; set; }
        public Position Position { get;set; }
    }
}
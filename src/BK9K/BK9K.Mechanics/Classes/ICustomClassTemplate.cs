using System.Collections.Generic;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Effects;

namespace BK9K.Mechanics.Classes
{
    public interface ICustomClassTemplate : IClassTemplate
    {
        public IEnumerable<Effect> LevelUpEffects { get; }
    }
}
using System;
using System.Collections.Generic;
using OpenRpg.Core.Classes;
using OpenRpg.Core.Effects;

namespace BK9K.Mechanics.Classes
{
    public class CustomClassTemplate : DefaultClassTemplate, ICustomClassTemplate
    {
        public IEnumerable<Effect> LevelUpEffects { get; set; } = Array.Empty<Effect>();
    }
}
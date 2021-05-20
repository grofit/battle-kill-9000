using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace BK9K.Framework.Effects
{
    public class CardEffects : IHasDataId, IHasLocaleDescription, IHasEffects
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
    }
}
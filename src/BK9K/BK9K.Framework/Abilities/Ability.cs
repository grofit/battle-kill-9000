using OpenRpg.Core.Common;

namespace BK9K.Framework.Abilities
{
    public class Ability : IHasDataId, IHasLocaleDescription
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
    }
}
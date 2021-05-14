using System;

namespace BK9K.Framework.Conventions
{
    public interface IHasUniqueId
    {
        public Guid UniqueId { get; set; }
    }
}
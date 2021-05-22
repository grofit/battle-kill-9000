using System;

namespace BK9K.Mechanics.Conventions
{
    public interface IHasUniqueId
    {
        public Guid UniqueId { get; set; }
    }
}
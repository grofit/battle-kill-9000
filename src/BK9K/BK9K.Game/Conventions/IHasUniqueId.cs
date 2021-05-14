using System;

namespace BK9K.Game.Conventions
{
    public interface IHasUniqueId
    {
        public Guid UniqueId { get; set; }
    }
}
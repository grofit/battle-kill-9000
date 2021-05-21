using System;
using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;

namespace BK9K.Cards.Genres.Conventions
{
    public abstract class GenericDataCard<T> : ICard
        where T : IHasLocaleDescription
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public abstract int CardType { get; }
        public T Data { get; }

        protected GenericDataCard(T data)
        { Data = data; }

        public virtual string NameLocaleId => Data.NameLocaleId;
        public virtual string DescriptionLocaleId => Data.DescriptionLocaleId;
        public abstract IEnumerable<Effect> Effects { get; }
    }
}
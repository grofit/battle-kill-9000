using System.Collections.Generic;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Types.Lookups;
using OpenRpg.Cards.Effects;
using OpenRpg.Combat.Abilities;

namespace BK9K.Game.Data.Datasets
{
    public class AbilityDataset : IDataset<Ability>
    {
        public List<Ability> GetDataset()
        {
            return new List<Ability>
            {
                MakeAttack()
            };
        }

        private Ability MakeAttack()
        {
            return new Ability
            {
                Id = AbilityLookups.Attack,
                NameLocaleId = "Attack",
                DescriptionLocaleId = "Uses the default weapon to attack a nearby unit"
            };
        }
    }
}
using System.Linq;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Requirements;

namespace BK9K.Mechanics.Requirements
{
    public class UnitRequirementChecker : IUnitRequirementChecker
    {
        public ICharacterRequirementChecker CharacterRequirementChecker { get; }

        public UnitRequirementChecker(ICharacterRequirementChecker characterRequirementChecker)
        {
            CharacterRequirementChecker = characterRequirementChecker;
        }

        public bool IsRequirementMet(Unit target, Requirement requirement)
        {
            if(requirement.RequirementType == CustomRequirementTypes.HasAbility)
            { return target.ActiveAbilities.Any(x => x.Id == requirement.AssociatedId); }
            
            if(requirement.RequirementType == CustomRequirementTypes.CanHealSelf)
            { return target.HasHealAbility(); }
            
            if(requirement.RequirementType == CustomRequirementTypes.CanHealOthers)
            { return target.HasHealOtherAbility(); }
            
            if(requirement.RequirementType == CustomRequirementTypes.CanAttack)
            { return target.HasActiveAttackAbility(); }

            return CharacterRequirementChecker.IsRequirementMet(target, requirement);
        }
    }
}
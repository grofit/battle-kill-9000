using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BK9K.Game.AI.Applicators.Models;
using BK9K.Game.AI.Extensions;
using BK9K.Game.AI.Modifiers;
using BK9K.Game.Data.Variables;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine;
using OpenRpg.AdviceEngine.Accessors;
using OpenRpg.AdviceEngine.Advisors;
using OpenRpg.AdviceEngine.Advisors.Applicators;
using OpenRpg.AdviceEngine.Keys;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;
using OpenRpg.Core.Requirements;

namespace BK9K.Game.AI.Applicators.Advisories
{
    public class ShouldUseAbilityAdviceApplicator : DefaultAdviceApplicator<Unit>
    {
        public Level Level { get; }

        public UtilityKey[] TargetUtilities = new[]
        {
            new UtilityKey(UtilityVariableTypes.EnemyDistance),
            new UtilityKey(UtilityVariableTypes.EnemyLowHealth),
            new UtilityKey(UtilityVariableTypes.IsAbilityDamaging),
            new UtilityKey(UtilityVariableTypes.IsWeak),
            new UtilityKey(UtilityVariableTypes.IsVulnerable)
        };

        public override IEnumerable<Requirement> Requirements { get; } = new[]
        {
            new Requirement { RequirementType = CustomRequirementTypes.CanAttack }
        };

        public ShouldUseAbilityAdviceApplicator(IRequirementChecker<Unit> requirementChecker, Level level) : base(requirementChecker)
        {
            Level = level;
        }

        public UnitAbility GetAbilityToUse(Unit unit, IUtilityVariables variables)
        {
            var abilityUtility = variables
                .GetRelatedUtilities(UtilityVariableTypes.IsAbilityDamaging)
                .OrderByDescending(x => x.Value)
                .First();

            return unit.GetAbility(abilityUtility.Key.RelatedId);
        }

        public int GetBestTargetForNonShapedAttack(IUtilityVariables variables)
        {
            return variables.GetBestRelatedIdFor(
                UtilityVariableTypes.EnemyDistance,
                UtilityVariableTypes.EnemyLowHealth);
        }

        public int GetBestTargetForShapedAttack(Unit unit, UnitAbility ability, IUtilityVariables variables)
        {
            var scoredEnemyDistances = variables.GetRelatedScoresFor(UtilityVariableTypes.EnemyDistance);

            var shapeLayouts = new[]
            {
                ability.Shape,
                ability.Shape.Rotate90(),
                ability.Shape.Rotate180(),
                ability.Shape.RotateN90()
            };

            var maxUnitsToHit = ability.Shape.ActiveCellCount();
            var bestEnemiesHit = 0;
            var bestEnemyId = 0;
            foreach (var enemyDistance in scoredEnemyDistances)
            {
                var enemy = Level.GetUnitById(enemyDistance.Key);
                foreach (var shape in shapeLayouts)
                {
                    var unitsHit = Level.GetAllUnitsInShape(enemy.Position, shape).Count();
                    if (unitsHit >= maxUnitsToHit) { return enemy.Id; }

                    if (unitsHit > bestEnemiesHit)
                    {
                        bestEnemiesHit = unitsHit;
                        bestEnemyId = enemy.Id;
                    }
                }
            }

            return bestEnemyId;
        }

        public AbilityWithTarget GetBestTarget(IHasDataId context, IUtilityVariables variables)
        {
            try
            {
                var unit = (context as Unit);
                var abilityToUse = GetAbilityToUse(unit, variables);

                if (abilityToUse.Shape.Equals(ShapePresets.Empty))
                {
                    var nonShapedTargetId = GetBestTargetForNonShapedAttack(variables);
                    return new AbilityWithTarget(nonShapedTargetId, abilityToUse.Id);
                }

                var shapedTargetId = GetBestTargetForShapedAttack(unit, abilityToUse, variables);
                return new AbilityWithTarget(shapedTargetId, abilityToUse.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public override IAdvice CreateAdvice(IAgent agent)
        {
            var modifiers = new[] { new AdditiveValueModifier(0.1f) };
            var contextAccessor = new ManualContextAccessor(agent.OwnerContext, agent.UtilityVariables, GetBestTarget);
            return new DefaultAdvice(AdviceVariableTypes.UseAbility, TargetUtilities, contextAccessor, modifiers);
        }
    }
}
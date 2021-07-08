using System.Linq;
using BK9K.Game.Extensions;
using BK9K.Game.Levels;
using BK9K.Mechanics.Abilities;
using BK9K.Mechanics.Extensions;
using BK9K.Mechanics.Types;
using BK9K.Mechanics.Units;
using OpenRpg.AdviceEngine.Modifiers;
using OpenRpg.AdviceEngine.Variables;
using OpenRpg.Core.Common;

namespace BK9K.Game.AI.Modifiers
{
    public class HasEnemiesInShapeModifier : IValueModifier
    {
        public Level Level { get; }
        public AbilityShape Shape { get; }

        private int _cachedMax = 0;
        private int _maxTargetableCount = 0;

        public HasEnemiesInShapeModifier(Level level, AbilityShape shape)
        {
            Level = level;
            Shape = shape;
            _maxTargetableCount = Shape.ActiveCellCount();
        }

        public int GetMaxSurroundingUnits(Unit unit)
        {
            var opposingFaction = unit.FactionType == FactionTypes.Player ? FactionTypes.Enemy : FactionTypes.Player;
            var enemies = Level.GetAllUnitsInFaction(opposingFaction).Where(x => !x.Unit.IsDead());

            var shapeLayouts = new[]
            {
                Shape,
                Shape.Rotate90(),
                Shape.Rotate180(),
                Shape.RotateN90()
            };

            var maxEffectedUnitCount = 0;
            foreach (var enemy in enemies)
            {
                foreach (var shape in shapeLayouts)
                {
                    var count = Level
                        .GetAllUnitsInShape(enemy.Unit.Position, shape)
                        .Count(x => x.Unit.FactionType == opposingFaction);

                    if(count >= _maxTargetableCount)
                    { return _maxTargetableCount; }

                    if (count > maxEffectedUnitCount)
                    { maxEffectedUnitCount = count; }
                }
            }

            return maxEffectedUnitCount;
        }
        
        public bool ShouldApply(IHasDataId ownerContext, IUtilityVariables utilityVariables)
        {
            var unitsCaughtInShape = GetMaxSurroundingUnits(ownerContext as Unit);
            _cachedMax = unitsCaughtInShape;
            return _cachedMax > 1;
        }

        public float ModifyValue(float currentValue, IHasDataId ownerContext, IUtilityVariables utilityVariables)
        { return currentValue * _cachedMax; }
    }
}
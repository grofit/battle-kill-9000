namespace BK9K.Mechanics.Abilities
{
    public class ShapePresets
    {
        public static AbilityShape Empty = new AbilityShape(0, new byte[0, 0], 1);
        
        public static AbilityShape AOE3x3 = new AbilityShape(3, new byte[3, 3]
        {
            {2,2,2},
            {2,1,2},
            {2,2,2}
        }, 1);
        
        public static AbilityShape TShape = new AbilityShape(3, new byte[3, 3]
        {
            {2,2,2},
            {0,2,0},
            {0,1,0}
        }, 1);
        
        public static AbilityShape VerticalLineShape = new AbilityShape(3, new byte[3, 3]
        {
            {0,2,0},
            {0,2,0},
            {0,1,0}
        }, 1);
        
        public static AbilityShape HorizontalLineShape = new AbilityShape(3, new byte[3, 3]
        {
            {0,0,0},
            {0,0,0},
            {2,1,2}
        }, 1);
    }
}
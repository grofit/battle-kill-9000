using BK9K.Mechanics.Grids;

namespace BK9K.Mechanics.Extensions
{
    public static class GridExtensions
    {
        public static GridSquare GetSquareAbove(this Grid grid, int x, int y)
        {
            return grid.Squares[(grid.XSize * y) + x];
        }
    }
}
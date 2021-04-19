using BK9K.Framework.Grids;

namespace BK9K.Framework.Extensions
{
    public static class GridExtensions
    {
        public static GridSquare GetSquareAbove(this Grid grid, int x, int y)
        {
            return grid.Squares[(grid.XSize * y) + x];
        }
    }
}
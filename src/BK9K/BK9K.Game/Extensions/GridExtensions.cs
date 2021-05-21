using BK9K.Game.Grids;

namespace BK9K.Game.Extensions
{
    public static class GridExtensions
    {
        public static GridSquare GetSquareAbove(this Grid grid, int x, int y)
        {
            return grid.Squares[(grid.XSize * y) + x];
        }
    }
}
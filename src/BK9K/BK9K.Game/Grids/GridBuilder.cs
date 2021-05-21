namespace BK9K.Game.Grids
{
    public class GridBuilder
    {
        private int _xSize = 3, _ySize = 3;
        private GridSquare[] _squares;

        public static GridBuilder Create()
        { return new GridBuilder(); }

        public GridBuilder WithSize(int xSize, int ySize)
        {
            _xSize = xSize;
            _ySize = ySize;
            return this;
        }

        public Grid Build()
        {
            return new Grid
            {
                Squares = new GridSquare[_xSize * _ySize],
                XSize = _xSize,
                YSize = _ySize
            };
        }
    }
}
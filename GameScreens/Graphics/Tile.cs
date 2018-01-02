using Microsoft.Xna.Framework;

namespace Gahame.GameScreens
{
    public class Tile
    {
        // tile number
        public Point ColumnRow;

        // Position on screen
        public Vector2 Position;

        // Constructor of tile
        public Tile()
        {
            ColumnRow = Point.Zero;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Gahame.GameScreens
{
    public class Tileset
    {
        // List of tiles
        public List<Tile> Tiles;

        // Texture of tiles
        public Texture2D Image;

        // tile amount
        public Vector2 TileAmount;

        // tile rectangle
        Rectangle sourceRect;

        // constructor
        public Tileset()
        {
            Tiles = new List<Tile>();
        }
        
        // Draw Tiles
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each tile
            for (int i = 0; i < Tiles.Count; i++)
            {
                sourceRect.X = Tiles[i].ColumnRow.X * sourceRect.Width;
                sourceRect.Y = Tiles[i].ColumnRow.Y * sourceRect.Height;

                spriteBatch.Draw(texture: Image,
                             position: Tiles[i].Position,
                             sourceRectangle: sourceRect);
            }
        }

        // Load the texture
        public void LoadTexture(ContentManager content, string path, Vector2 tileAmount)
        {
            // Load image from path
            Image = content.Load<Texture2D>(path);

            // make rectangle for that texture and size
            sourceRect = new Rectangle(0, 0, (int)(Image.Width / tileAmount.X), (int)(Image.Height / tileAmount.Y));
            TileAmount = tileAmount;
        }
    }
}

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
        public Rectangle sourceRect;

        // depth 
        public float Depth;

        // SCreen
        GameScreen screen;

        // constructor
        public Tileset(GameScreen screen)
        {
            Tiles = new List<Tile>();
            this.screen = screen;

            TileAmount = new Vector2(0, 0);
            Depth = .5f;
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
                                 sourceRectangle: sourceRect,
                                 layerDepth: Depth);
            }
        }

        // Load the texture
        public virtual void LoadTexture(string path)
        {
            // Load image from path
            Image = screen.content.Load<Texture2D>(path);

            // make rectangle for that texture and size
            sourceRect = new Rectangle(0, 0, (int)(Image.Width / TileAmount.X), (int)(Image.Height / TileAmount.Y));
        }
    }
}

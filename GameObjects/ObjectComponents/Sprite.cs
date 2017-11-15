using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Gahame.GameObjects;
using Gahame.GameScreens;

namespace Gahame.GameObjects.ObjectComponents
{

    public class Sprite : ObjectComponent
    {

        // Array of images
        List<Texture2D> images;

        // Current Image
        public float CurrentImage;

        // ImageSpeed
        public float ImageSpeed;

        // Origin
        public Vector2 SpriteOrigin;

        // Sclae
        public Vector2 SpriteScale;

        // depth
        public Single Depth;

        // Constructor boiis
        public Sprite(GameObject gameObject) : base(gameObject)
        {
            // Fixes drawable and updatable thing
            Drawable = true;
            Updatable = false;

            // Creates array of images
            images = new List<Texture2D>();
            CurrentImage = 0;
            ImageSpeed = 0;
            Depth = .5f;

            SpriteOrigin = new Vector2(0, 0);
            SpriteScale = new Vector2(1, 1);
        }

        // Draws the sprite
        public override void Draw(SpriteBatch spriteBatch)
        {
            // updates current image
            if (ImageSpeed > 0)
            {
                if (CurrentImage + ImageSpeed < images.Count)
                {
                    CurrentImage += ImageSpeed;
                } else
                {
                    CurrentImage = CurrentImage + ImageSpeed - images.Count;
                }
            } else if (ImageSpeed < 0) 
            {
                if (CurrentImage + ImageSpeed >= 0)
                {
                    CurrentImage += ImageSpeed;
                } else
                {
                    CurrentImage = images.Count + (CurrentImage + ImageSpeed); 
                }
            }

            // Draws the current image
            spriteBatch.Draw(images[(int)CurrentImage],
                gameObject.Position,
                origin: SpriteOrigin,
                scale: absVec(SpriteScale),
                layerDepth: Depth,
                effects: (SpriteScale.X < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            
        }

        // Add image to array of images
        public void AddImage(string path)
        {
            // Loads image from current screens contentmanager
            images.Add(gameObject.screen.content.Load<Texture2D>(path));
        }
        // Add Texture to array of images
        public void AddImage(Texture2D texture)
        {
            // Loads image from current screens contentmanager
            images.Add(texture);
        }

        Vector2 absVec(Vector2 vec)
        {
            vec.X = Math.Abs(vec.X);
            vec.Y = Math.Abs(vec.Y);
            return vec;
        }

    }
}

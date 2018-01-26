using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Gahame.GameUtils;

namespace Gahame.GameObjects.ObjectComponents
{

    public class Sprite : ObjectComponent
    {

        // Array of images
        public List<Texture2D> Images;

        // Current Image
        public float CurrentImage;

        // ImageSpeed
        public float ImageSpeed;

        // Origin
        public Vector2 SpriteOrigin;

        // Sclae
        public Vector2 SpriteScale;

        // Rotation
        public float SpriteRotation;

        // depth
        public float Depth;

        // Constructor boiis
        public Sprite(GameObject gameObject) : base(gameObject)
        {
            // Fixes drawable and updatable thing
            Drawable = true;
            Updatable = true;
            DrawableGUI = false;

            // Creates array of images
            Images = new List<Texture2D>();
            CurrentImage = 0;
            ImageSpeed = 0;
            SpriteRotation = 0;

            // Depth should bee between .5 and .8
            Depth = .5f;

            SpriteOrigin = new Vector2(0, 0);
            SpriteScale = new Vector2(1, 1);
        }

        // Update sprite
        public override void Update(GameTime gameTime)
        {
            // Current imagespeed (accounting for gamespeed)
            float currentImageSpeed = ImageSpeed * GahameController.GameSpeed;

            // updates current image
            if (currentImageSpeed > 0)
            {
                if (CurrentImage + currentImageSpeed < Images.Count)
                {
                    CurrentImage += currentImageSpeed;
                }
                else
                {
                    CurrentImage = CurrentImage + currentImageSpeed - Images.Count;
                }
            }
            else if (currentImageSpeed < 0)
            {
                if (CurrentImage + currentImageSpeed >= 0)
                {
                    CurrentImage += currentImageSpeed;
                }
                else
                {
                    CurrentImage = Images.Count + (CurrentImage + currentImageSpeed);
                }
            }
        }

        // Draws the sprite
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draws the current image
            spriteBatch.Draw(Images[(int)CurrentImage],
                            gameObject.Position,
                            origin: SpriteOrigin,
                            scale: AbsVec(SpriteScale),
                            layerDepth: Depth,
                            rotation: SpriteRotation,
                            effects: ((SpriteRotation > (float)(Math.PI/2) ? SpriteScale.X > 0 : SpriteScale.X < 0)) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        // Add image to array of images
        public virtual void AddImage(string path)
        {
            // Loads image from current screens contentmanager
            Images.Add(gameObject.screen.content.Load<Texture2D>(path));
        }
        // Add Texture to array of images
        public void AddImage(Texture2D texture)
        {
            // Loads image from current screens contentmanager
            Images.Add(texture);
        }

        Vector2 AbsVec(Vector2 vec)
        {
            vec.X = Math.Abs(vec.X);
            vec.Y = Math.Abs(vec.Y);
            return vec;
        }

    }
}

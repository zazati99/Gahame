using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Gahame.GameScreens
{
    public class ScreenBackground
    {
        // Texture that will be drawn
        public Texture2D Image;

        // Depth (should be between 0.9 and 1, between 0.1 and 0.3 for forground)
        public float Depth;

        // Position on screen
        public Vector2 Position;

        // Does it follow the camera?
        public bool FollowCamera;

        // Repeat?
        public bool RepeatX;
        public bool RepeatY;

        // Amount of paralaxing (1 is static image);
        public float ParalaxAmount;

        // screen that this background is in
        GameScreen screen;

        // constructorino
        public ScreenBackground(GameScreen screen)
        {
            // Le screen
            this.screen = screen;

            // Default values
            RepeatX = false;
            RepeatY = false;
            FollowCamera = false;
            ParalaxAmount = 1;
            Depth = 1;
        }

        // Load the Texture
        public void LoadTexture(ContentManager content, string path)
        {
            // Load the texture
            Image = content.Load<Texture2D>(path);
        }

        // Dispose le image
        public void UnloadContent()
        {
            Image.Dispose();
        }

        // Draw function
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < (RepeatY ? (int)(screen.ScreenSize.Y / Image.Height) + 2 : 1); y++)
            {
                for (int x = 0; x < (RepeatX ? (int)(screen.ScreenSize.X / Image.Width) + 2 : 1); x++)
                {
                    // Position vector som ska bli memead
                    Vector2 pos;

                    // Do calculation memes if it's following camera
                    if (FollowCamera)
                    {
                        pos = (Camera.PositionOnScreen(new Vector2((RepeatX ? (-Image.Width + (Position.X % Image.Width) / ParalaxAmount) : Position.X) + x * Image.Width / ParalaxAmount,
                                                                   (RepeatY ? (-Image.Height + (Position.Y % Image.Height) / ParalaxAmount) : Position.Y) + y * Image.Height / ParalaxAmount)) * ParalaxAmount);
                    }
                    // If Not follow camera, do other calculation memes
                    else
                    {
                        pos = new Vector2((RepeatX ? -Image.Width + (Position.X % Image.Width) : Position.X) + x * Image.Width,
                            (RepeatY ? -Image.Height + (Position.Y % Image.Height) : Position.Y) + y * Image.Height);
                    }

                    // Draw the image at position
                    spriteBatch.Draw(Image, pos, layerDepth: Depth);
                }
            }
        }

    }
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using System;

using Gahame.GameObjects;

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
            this.screen = screen;
            RepeatX = false;
            RepeatY = false;
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
                        pos = (CameraController.PositionOnScreen(new Vector2((RepeatX ? -Image.Width / ParalaxAmount + (Position.X % Image.Width / ParalaxAmount) : Position.X) + x * Image.Width / ParalaxAmount,
                            (RepeatY ? -Image.Height / ParalaxAmount + (Position.Y % Image.Height / ParalaxAmount) : Position.Y) + y * Image.Height / ParalaxAmount)) * ParalaxAmount);
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


            /*
            // Check if it is following camera or NO?
            if (FollowCamera)
            {
                // If camera follow is true it will draw it relative to camera and then multiplied by paralax ammount
                spriteBatch.Draw(Image,
                CameraController.PositionOnScreen(Position) * ParalaxAmount,
                layerDepth: Depth
                );
            } else
            {
                // Check if it'w repeating on x or y axis
                if (RepeatX || RepeatY)
                {
                    // checks if it's repeating on both x and y axis
                    if (RepeatX && RepeatY)
                    {
                        // Repeat on both x and y axis
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.Y/Image.Height)+1; i++)
                        {
                            for (int j = 0; j < Math.Ceiling(screen.ScreenSize.X / Image.Width)+1; j++)
                            {
                                spriteBatch.Draw(Image,
                                    new Vector2(-Image.Width, -Image.Height) + new Vector2(Position.X % Image.Width, Position.Y % Image.Height) + new Vector2(j * Image.Width, i * Image.Height),
                                    layerDepth: Depth
                                    );
                            }
                        }
                    } else if (RepeatX) // Checks if it's only repeating on X axis
                    {
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.X/Image.Width + 1); i++)
                        {
                            spriteBatch.Draw(Image,
                                new Vector2((Position.X % Image.Width) - Image.Width, Position.Y) + new Vector2(i * Image.Width, 0),
                                layerDepth: Depth
                                );
                        }
                    } else // otherwise it's only repeating on Y axis
                    {
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.Y / Image.Height+1); i++)
                        {
                            spriteBatch.Draw(Image,
                                new Vector2(Position.X, (Position.Y % Image.Height)-Image.Height) + new Vector2(0, i * Image.Height),
                                layerDepth: Depth
                                );
                        }
                    }
                } else // Just Draw the image otherwise
                {
                    spriteBatch.Draw(Image,
                        Position,
                        layerDepth: Depth
                        );
                }
            }
            */
        }

        // Draws the image
        void drawImage(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.Draw(Image,
                pos,
                layerDepth: Depth
                );
        }

    }
}

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
            // Check if it is following camera or NO?
            if (FollowCamera)
            {
                spriteBatch.Draw(Image,
                CameraController.PositionOnScreen(Position) * ParalaxAmount,
                layerDepth: Depth
                );
            } else
            {
                if (RepeatX || RepeatY)
                {
                    if (RepeatX && RepeatY)
                    {
                        // Repeat on both x and y axis
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.Y/Image.Height); i++)
                        {
                            for (int j = 0; j < Math.Ceiling(screen.ScreenSize.X / Image.Width); j++)
                            {
                                spriteBatch.Draw(Image,
                                    Position + new Vector2(j * Image.Width, i * Image.Height),
                                    layerDepth: Depth
                                    );
                            }
                        }
                    } else if (RepeatX)
                    {
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.X/Image.Width); i++)
                        {
                            spriteBatch.Draw(Image,
                                Position + new Vector2(i * Image.Width, 0),
                                layerDepth: Depth
                                );
                        }
                    } else
                    {
                        for (int i = 0; i < Math.Ceiling(screen.ScreenSize.Y / Image.Height); i++)
                        {
                            spriteBatch.Draw(Image,
                                Position + new Vector2(0, i * Image.Height),
                                layerDepth: Depth
                                );
                        }
                    }
                } else
                {
                    spriteBatch.Draw(Image,
                        Position,
                        layerDepth: Depth
                        );
                }
            }
        }

    }
}

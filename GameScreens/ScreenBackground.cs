using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Gahame.GameObjects;

namespace Gahame.GameScreens
{
    public class ScreenBackground
    {
        // Texture that will be drawn
        public Texture2D Image;

        // Depth (should be between 0.9 and 1)
        public float Depth;

        // Position on screen
        public Vector2 Position;

        // Does it follow the camera?
        public bool FollowCamera;

        // Amount of paralaxing (1 is static image);
        public float ParalaxAmount;

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
            spriteBatch.Draw(Image,
                FollowCamera ? CameraController.PositionOnScreen(Position) * ParalaxAmount : Position,
                layerDepth: Depth
                );
        }

    }
}

using Gahame.GameUtils;
using Gahame.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameScreens
{
    public class ScreenLoadArea
    {
        // Name of screen that this area loads
        public string ScreenPath;

        // Position
        public Vector2 Position;

        // Size
        public Vector2 Size;

        // Test draw (only testeroni thing)
        public void Draw(SpriteBatch spriteBatch)
        {
            ShapeRenderer.DrawRectangle(spriteBatch, Position, Size, Color.Red, 1);
        }

        // Collision with player and load screen
        public void CheckCollisionWithPlayer(Vector2 playerPosition)
        {
            if (Position.X < playerPosition.X &&
                Position.X + Size.X > playerPosition.X &&
                Position.Y < playerPosition.Y &&
                Position.Y + Size.Y > playerPosition.Y)
            {
                if (!ScreenManager.Instance.ScreenTransition)
                {
                    if (ScreenManager.Instance.NextScreen == null)
                    {
                        ScreenManager.Instance.LoadNextScreen(ScreenPath);
                    }
                    else if (ScreenManager.Instance.NextScreen.Name != ScreenPath)
                    {
                        ScreenManager.Instance.LoadNextScreen(ScreenPath);
                    }
                }
            }
        }
    }
}

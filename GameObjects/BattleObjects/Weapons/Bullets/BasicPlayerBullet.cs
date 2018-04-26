using Gahame.GameObjects.ObjectComponents;
using Gahame.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{
    public class BasicPlayerBullet : PlayerBullet
    {

        public BasicPlayerBullet(GameScreen screen, Vector2 speed) : base(screen)
        {

            Texture2D rect = new Texture2D(Game1.Graphics.GraphicsDevice, 2, 2);

            Color[] data = new Color[2 * 2];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            rect.SetData(data);

            Sprite sprite = new Sprite(this);
            sprite.AddImage(rect);

            Components.Add(sprite);

            Physics physics = new Physics(this);
            physics.GravityEnabled = false;
            physics.Solid = false;
            physics.Velocity = speed;
            Components.Add(physics);

        }

    }
}

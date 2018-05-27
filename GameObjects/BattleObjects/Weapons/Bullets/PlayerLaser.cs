using System;

using Gahame.GameScreens;
using Gahame.GameUtils;
using Gahame.GameObjects.ObjectComponents.Colliders;
using Gahame.GameObjects.ObjectComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{
    public class PlayerLaser : PlayerBullet
    {

        HitBox hb;
        Vector2 speed;
        Vector2 startPosition;

        Timer deleteTimer;

        float thickness;

        // OOof
        public PlayerLaser(GameScreen screen, Vector2 speed, Vector2 startPosition) : base(screen)
        {

            hb = new HitBox(new Vector2(4, 4), this);
            this.speed = speed;

            Position = startPosition;
            this.startPosition = startPosition;

            thickness = 8;

            deleteTimer = new Timer(20, false);

        }

        public override void Update(GameTime gameTime)
        {

            if (deleteTimer.CheckAndTick())
            {
                DestroyObject();
            }

            Position = startPosition;
            for (int i = 0; i < 20; i++)
            {
                Position += speed;
                if (hb.PlaceMeeting<WallObject>(Position))
                {
                    break;
                }
            }

            thickness = MyMaths.Lerp(thickness, 0, .15f);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            Vector2 thicknessCorrection = new Vector2((thickness / 2) * Math.Sign(speed.Y), (thickness / 2) * -Math.Sign(speed.X));
            ShapeRenderer.DrawLine(spriteBatch, startPosition + thicknessCorrection, Position + speed + thicknessCorrection, Color.Red, thickness, .8f);

            Vector2 random = new Vector2(MyMaths.RandomInRange(-thickness / 4, thickness / 4), MyMaths.RandomInRange(-thickness / 4, thickness / 4));
            ShapeRenderer.DrawLine(spriteBatch, startPosition + random + thicknessCorrection/3, Position + speed/2 + random + thicknessCorrection/3, Color.Pink, thickness/3, .8f);

            base.Draw(spriteBatch);
        }

    }
}

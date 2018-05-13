using System;

using Gahame.GameScreens;
using Gahame.GameUtils;
using Microsoft.Xna.Framework;

namespace Gahame.GameObjects
{
    public class PlayerBullet : GameObject
    {

        Timer deleteTimer;

        // OOof
        public PlayerBullet(GameScreen screen) : base(screen)
        {

            deleteTimer = new Timer(20, false);

        }

        public override void Update(GameTime gameTime)
        {
            if (deleteTimer.CheckAndTick())
            {
                DestroyObject();
            }
            base.Update(gameTime);
        }

    }
}

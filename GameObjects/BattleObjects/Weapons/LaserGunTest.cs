using System;

using Gahame.GameScreens;
using Gahame.GameUtils;

using Microsoft.Xna.Framework;

namespace Gahame.GameObjects
{
    public class LaserGunTest : PlayerWeapon
    {

        // shut
        public override void Shoot(GameScreen screen, Vector2 direction)
        {
            if (GameInput.InputPressed(GameInput.ShootInput))
            {
        
                Vector2 speed = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y)) * MyMaths.Normalize(direction.X, direction.Y) * direction * 6;
                Vector2 bulletPos = screen.Player.Position;
                screen.GameObjects.Add(new PlayerLaser(screen, speed, bulletPos));

            }
        }

    }
}

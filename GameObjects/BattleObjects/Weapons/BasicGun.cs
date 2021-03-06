﻿using System;

using Gahame.GameScreens;
using Gahame.GameUtils;

using Microsoft.Xna.Framework;

namespace Gahame.GameObjects
{
    public class BasicGun : PlayerWeapon
    {

        // shut
        public override void Shoot(GameScreen screen, Vector2 direction)
        {
            if (GameInput.InputPressed(GameInput.ShootInput))
            {
                BasicPlayerBullet bullet = new BasicPlayerBullet(screen, new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y)) * MyMaths.Normalize(direction.X, direction.Y) * direction * 6);
                bullet.Position = screen.Player.Position;

                screen.GameObjects.Add(bullet);

                screen.ScreenEffects.Add(new CameraShakeEffect(screen, 2, 4));
            }
        }

    }
}

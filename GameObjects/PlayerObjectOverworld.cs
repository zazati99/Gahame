﻿using System;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;
using Gahame.GameObjects.ObjectComponents.DialogueSystem;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{

    // Overworld version of player object
    public class PlayerObjectOverworld : GameObject
    {
        // Componenst
        Sprite sprite;
        HitBox hitBox;
        Physics physics;

        // Controlls variablees
        float maxSpeed;
        float accelerationSpeed;
        float slowDownSpeed;
        float speedInDirection;

        // normalized vector
        Vector2 norm;

        // Constructor stufferoo for playerino
        public PlayerObjectOverworld(GameScreen screen) : base(screen)
        {
            // Cool sprite stuff
            sprite = new Sprite(this);
            sprite.Depth = .1f;
            sprite.AddImage("Sprites/Player/playerBattleScreenStill");
            sprite.SpriteOrigin = new Vector2(24, 24);
            Components.Add(sprite);

            // HitBox COmponent 
            hitBox = new HitBox(this);
            hitBox.Colliders.Add(new BoxCollider(new Vector2(14, 13)));
            hitBox.Colliders[0].Offset.X = -7;
            hitBox.Colliders[0].Offset.Y = 11;
            Components.Add(hitBox);

            // Physics
            physics = new Physics(this);
            physics.Solid = true;
            physics.GravityEnabled = false;
            Components.Add(physics);

            // Camera
            screen.CamController.target = this;
            screen.CamController.CamOffset.Y = -16;
            screen.CamController.MoveAmount = new Vector2(.2f, .2f);
            //screen.CamController.Static = true;
            //screen.CamController.SetPosition(new Vector2(330, 100));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            slowDownSpeed = .25f;
            speedInDirection = 0;
        }


        // Update player
        public override void Update(GameTime gameTime)
        {
            // Normalized vector
            if (GameControlls.ControllerMode)
            {
                //norm = MyMaths.Normalize(GameControlls.LeftStickX, GameControlls.LeftStickY);
                norm.X = GameControlls.LeftStickX;
                norm.Y = GameControlls.LeftStickY;
                if (Math.Abs(norm.X) > 0.70f || Math.Abs(norm.Y) > 0.70f)
                    norm = MyMaths.Normalize(GameControlls.LeftStickX, GameControlls.LeftStickY);
            } else
            {
                norm = MyMaths.Normalize((GameControlls.RightCD ? 1 : 0) - (GameControlls.LeftCD ? 1 : 0), (GameControlls.DownCD ? 1 : 0) - (GameControlls.UpCD ? 1 : 0));
            }

            // Approach max xspeed
            if (GameControlls.RightCD || GameControlls.LeftCD)
            {
                // Approach xSPeed;
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, maxSpeed * norm.X, accelerationSpeed * GahameController.GameSpeed);
            }
            else physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, slowDownSpeed * GahameController.GameSpeed);

            // approach max yspeed;
            if (GameControlls.UpCD || GameControlls.DownCD)
            {
                // Approach xSPeed;
                physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, maxSpeed * norm.Y, accelerationSpeed * GahameController.GameSpeed);
            }
            else physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, 0, slowDownSpeed * GahameController.GameSpeed);

            // Update component last
            base.Update(gameTime);
        }

        // Draw player
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Speed test
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.X.ToString()).X / 2, 32), Color.Black);
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.Y.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.Y.ToString()).X / 2, 42), Color.Black);
        }
    }
}

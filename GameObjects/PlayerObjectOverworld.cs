using System;

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
            screen.CamController.Target = this;
            screen.CamController.MovementAmount = new Vector2(.2f, .2f);

            // Depth fix
            Components.Add(new OverworldDepthFix(this));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            slowDownSpeed = .25f;
        }


        // Update player
        public override void Update(GameTime gameTime)
        {
            // Normalized vector
            if (GameInput.ControllerMode)
            {
                //norm = MyMaths.Normalize(GameControlls.LeftStickX, GameControlls.LeftStickY);
                norm.X = GameInput.LeftStickX;
                norm.Y = GameInput.LeftStickY;
                if (Math.Abs(norm.X) > 0.70f || Math.Abs(norm.Y) > 0.70f)
                    norm = MyMaths.Normalize(GameInput.LeftStickX, GameInput.LeftStickY);
            } else
            {
                norm = MyMaths.Normalize((GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0), (GameInput.DownCD ? 1 : 0) - (GameInput.UpCD ? 1 : 0));
            }

            // Approach max xspeed
            if (GameInput.RightCD || GameInput.LeftCD)
            {
                // Approach xSPeed;
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, maxSpeed * norm.X, accelerationSpeed * GahameController.GameSpeed);
            }
            else physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, slowDownSpeed * GahameController.GameSpeed);

            // approach max yspeed;
            if (GameInput.UpCD || GameInput.DownCD)
            {
                // Approach xSPeed;
                physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, maxSpeed * norm.Y, accelerationSpeed * GahameController.GameSpeed);
            }
            else physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, 0, slowDownSpeed * GahameController.GameSpeed);

            // Activate Object
            if (GameInput.ActivateCD)
            {
                // Insane
                if (hitBox.InstancePlace<ActivatableObject>(Position) is ActivatableObject o)
                {
                    o.Activate();
                }

                screen.ScreenEffects.Add(new CameraShakeEffect(screen, 3, 20));
            }

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

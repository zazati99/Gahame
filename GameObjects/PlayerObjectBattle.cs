using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.DialogueSystem;
using Gahame.GameObjects.ObjectComponents.Colliders;

namespace Gahame.GameObjects
{
    public class PlayerObjectBattle : PlayerObject
    {
        // Componenst
        Sprite sprite;
        HitBox hitBox;
        Physics physics;

        // Controlls variablees
        float jumpHeight;
        float minJumpHeight;

        float maxSpeed;
        float accelerationSpeed;
        float airAccelerationSpeed;

        float slowDownSpeed;
        float airSlowDownSpeed;

        public bool Jumping;

        // Constructor stufferoo for playerino
        public PlayerObjectBattle(GameScreen screen) : base(screen)
        {
            // Cool sprite stuff
            sprite = new Sprite(this);
            sprite.Depth = .1f;
            sprite.AddImage("Sprites/Player/playerBattleScreenStill");
            sprite.SpriteOrigin = new Vector2(24, 24);
            Components.Add(sprite);

            // HitBox COmponent 
            hitBox = new HitBox(this);
            hitBox.Colliders.Add(new BoxCollider(new Vector2(14, 43)));
            hitBox.Colliders[0].Offset.X = -7;
            hitBox.Colliders[0].Offset.Y = -19;
            Components.Add(hitBox);

            // Physics
            physics = new Physics(this);
            physics.Solid = true;
            physics.GravityEnabled = true;
            Components.Add(physics);

            // Camera
            screen.CamController.Target = this;
            screen.CamController.MovementAmount = new Vector2(.2f, .01f);
            //screen.CamController.Static = true;
            //screen.CamController.SetPosition(new Vector2(330, 100));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            airAccelerationSpeed = accelerationSpeed * 0.75f;

            slowDownSpeed = .25f;
            airSlowDownSpeed = slowDownSpeed * 0.5f;

            jumpHeight = 4.5f;
            minJumpHeight = jumpHeight / 2;

            Jumping = false;
        }

        // Update stufferino
        public override void Update(GameTime gameTime)
        {
            // Start thing
            StartUpdate();

            // Do controlls for player if it's not a cutscene
            if (!GahameController.CutScene)
            {
                // Walking left and right
                if (GameInput.RightCD || GameInput.LeftCD)
                    WalkHorizontal((GameInput.ControllerMode ? GameInput.AbsLeftStickX : 1) * ((GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0)) * maxSpeed);

                // Jumping
                if (GameInput.JumpBufferCD) Jump();

                // Stop Jump things
                Jumping = GameInput.JumpHeld;

                // Stop jump
                if (!GameInput.JumpHeld) StopJump();

                // Interact with object
                if (GameInput.ActivateCD)
                {
                    Dialogue d = hitBox.DialogueMeeting(Position + sprite.SpriteScale);
                    if (d != null) d.StartDialogue();
                }
            }

            // Updates Components last*/
            base.Update(gameTime);

            if (physics.Grounded)
            {
                screen.CamController.MovementAmount.Y = MyMaths.Lerp(screen.CamController.MovementAmount.Y, .075f, .1f * GahameController.GameSpeed);
            } else
            {
                screen.CamController.MovementAmount.Y = MyMaths.Lerp(screen.CamController.MovementAmount.Y, .020f, .1f * GahameController.GameSpeed);
            }
        }

        // dDrawerino
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Speed test
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.X.ToString()).X / 2, 32), Color.Black);
        }

        #region Movement
        // Walk horizontaly
        public override void WalkHorizontal(float targetSpeed)
        {
            // lerp the sprite scale (prob wont keep)
            sprite.SpriteScale.X = MyMaths.Lerp(sprite.SpriteScale.X,
                Math.Sign(targetSpeed),
                .25f * GahameController.GameSpeed * (GameInput.ControllerMode && !GahameController.CutScene ? Math.Abs(GameInput.AbsLeftStickX) : 1));

            // keeps it from memeing maxSpeed
            targetSpeed = MyMaths.Clamp(targetSpeed, -maxSpeed, maxSpeed);

            // Approach the target speed
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, targetSpeed,
                GahameController.GameSpeed * (physics.Grounded ? accelerationSpeed : airAccelerationSpeed));

            // You be walking
            WalkingHorizontal = true;
        }

        // Stop walking
        public override void StopHorizontal()
        {
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, GahameController.GameSpeed * (physics.Grounded ? slowDownSpeed : airSlowDownSpeed));
        }

        // Jump
        public void Jump()
        {
            if (physics.Grounded)
            {
                physics.Velocity.Y = -jumpHeight * Math.Sign(Physics.Gravity);
                Jumping = true;
            }
        }

        // Stop the jump
        public void StopJump()
        {
            if (physics.Velocity.Y < 0)
            {
                physics.Velocity.Y = Math.Max(physics.Velocity.Y, -minJumpHeight);
            }
        }
        #endregion

    }
}

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
        HitBox hitBox;
        Physics physics;

        // Sprites
        SpriteManager spriteManager;
        float imageScale;

        // Controlls variablees
        float jumpHeight;
        float minJumpHeight;

        float maxSpeed;
        float accelerationSpeed;
        float airAccelerationSpeed;

        float slowDownSpeed;
        float airSlowDownSpeed;

        public bool Jumping;
        int jumpBuffer;

        // TEST
        PlayerWeapon weapon;

        // Constructor stufferoo for playerino
        public PlayerObjectBattle(GameScreen screen) : base(screen)
        {
            // Cool sprite stuff
            imageScale = 1;
            Sprite sprStill = new Sprite(this);
            sprStill.Depth = .1f;
            sprStill.AddImage("Sprites/Player/playerBattleScreenStill");
            sprStill.SpriteOrigin = new Vector2(24, 24);

            Sprite sprMoving = new Sprite(this);
            sprMoving.Depth = .1f;
            sprMoving.AddImage("Sprites/Player/playerBattleScreenStill");
            sprMoving.AddImage("Sprites/Player/playerBattleMoving");
            sprMoving.ImageSpeed = .1f;
            sprMoving.SpriteOrigin = new Vector2(24, 24);

            Sprite sprJumping = new Sprite(this);
            sprJumping.Depth = .1f;
            sprJumping.AddImage("Sprites/Player/playerBattleMoving");
            sprJumping.SpriteOrigin = new Vector2(24, 24);

            spriteManager = new SpriteManager(this);
            spriteManager.AddSprite("Still", sprStill);
            spriteManager.AddSprite("Moving", sprMoving);
            spriteManager.AddSprite("Jumping", sprJumping);
            Components.Add(spriteManager);

            // HitBox COmponent 
            hitBox = new HitBox(this);
            hitBox.Colliders.Add(new BoxCollider(new Vector2(14, 43)));
            hitBox.Colliders[0].Offset.X = -7;
            hitBox.Colliders[0].Offset.Y = -19;
            hitBox.Solid = true;
            hitBox.Priority = 0;
            Components.Add(hitBox);

            // Physics
            physics = new Physics(this);
            physics.Solid = true;
            physics.GravityEnabled = true;
            Components.Add(physics);

            // Camera
            screen.CamController.Target = this;
            screen.CamController.MovementAmount = new Vector2(.2f, .05f);
            //screen.CamController.Static = true;
            //screen.CamController.SetPosition(new Vector2(330, 100));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            airAccelerationSpeed = accelerationSpeed * 0.75f;

            slowDownSpeed = 0.25f;
            airSlowDownSpeed = slowDownSpeed * 0.5f;

            jumpHeight = 4.5f;
            minJumpHeight = jumpHeight / 2;

            Jumping = false;
            jumpBuffer = 0;

            // TEST
            weapon = new LaserGunTest();
        }

        // Update stufferino
        public override void Update(GameTime gameTime)
        {
            // Start updateing
            StartUpdate();

            // Im sorry my child
            if (!GahameController.CutScene)
            {
                // Walking left and right
                if (!GameInput.InputDown(GameInput.stopInput))
                {
                    if (GameInput.ControllerMode)
                    {

                        if (GameInput.PlatformerMovementStickX != 0)
                            WalkHorizontal(Math.Sign(GameInput.PlatformerMovementStickX) * maxSpeed);

                    } else
                    {

                        bool rightKey = GameInput.InputDown(GameInput.RightKey);
                        bool leftKey = GameInput.InputDown(GameInput.LeftKey);
                        if (rightKey || leftKey)
                            WalkHorizontal(((rightKey ? 1 : 0) - (leftKey ? 1 : 0)) * maxSpeed);

                    }
                }

                // Jumping
                jumpBuffer--;
                if (GameInput.InputPressed(GameInput.JumpInput)) jumpBuffer = 3;
                if (jumpBuffer > 0) Jump();

                // Stop Jump things
                //Jumping = GameInput.JumpHeld;
                Jumping = GameInput.InputDown(GameInput.JumpInput);

                // Stop jump
                if (!GameInput.InputDown(GameInput.JumpInput)) StopJump();

                // Interact with object
                if (GameInput.ActivateCD)
                {
                    Dialogue d = hitBox.DialogueMeeting(Position + new Vector2(imageScale, 0));
                    if (d != null) d.StartDialogue();
                }

                if (GameInput.InputDown(GameInput.ShootInput))
                {
                    // INSANE
                    Vector2 speedVec = Vector2.Zero;

                    if (GameInput.ControllerMode)
                    {

                        if (GameInput.PlatformerMovementStickX != 0)
                        {
                            speedVec = new Vector2(Math.Sign(GameInput.PlatformerMovementStickX), -Math.Sign(GameInput.shootStickY));
                        } else
                        {
                            speedVec = new Vector2(Math.Sign(GameInput.shootStickX), -Math.Sign(GameInput.shootStickY));
                        }

                    }
                    else
                    {
                        speedVec = new Vector2((GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0), (GameInput.DownCD ? 1 : 0) - (GameInput.UpCD ? 1 : 0));
                    }

                    if (speedVec == Vector2.Zero) speedVec = new Vector2(Math.Sign(imageScale), 0);

                    weapon.Shoot(screen, speedVec);
                }

            }

            // I dont even know anymore
            base.Update(gameTime);

            if (!physics.Grounded) spriteManager.ChangeSprite("Jumping");

            spriteManager.ChangeScaleOnAllSprites(new Vector2(imageScale, 1));
        }

        // dDrawerino
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Speed test
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.X.ToString()).X / 2, 42), Color.Black);
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.Y.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.Y.ToString()).X / 2, 32), Color.Black);
        }

        #region Movement
        // Walk horizontaly
        public override void WalkHorizontal(float targetSpeed)
        {
            // lerp the sprite scale (prob wont keep)
            imageScale = MyMaths.Lerp(imageScale,
                Math.Sign(targetSpeed),
                .25f * GahameController.GameSpeed);

            // keeps it from memeing maxSpeed
            targetSpeed = MyMaths.Clamp(targetSpeed, -maxSpeed, maxSpeed);

            // Approach the target speed
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, targetSpeed,
                GahameController.GameSpeed * (physics.Grounded ? accelerationSpeed : airAccelerationSpeed));

            // Sprite things
            if (spriteManager.CurrentSprite != "Moving")
            {
                spriteManager.ChangeSprite("Moving");
                spriteManager.GetSprite("Moving").CurrentImage = 0;
            }
            spriteManager.GetSprite("Moving").ImageSpeed = .1f * Math.Abs(physics.Velocity.X / maxSpeed);

            // You be walking
            WalkingHorizontal = true;
        }

        // Stop walking
        public override void StopHorizontal()
        {
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, GahameController.GameSpeed * (physics.Grounded ? slowDownSpeed : airSlowDownSpeed));
            spriteManager.ChangeSprite("Still");
        }

        // Jump
        public void Jump()
        {
            if (physics.Grounded)
            {
                physics.Velocity.Y = -jumpHeight * Math.Sign(Physics.Gravity.Y);
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

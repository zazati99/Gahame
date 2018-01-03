using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameScreens;
using Gahame.GameUtils;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;
using Gahame.GameObjects.ObjectComponents.DialogueSystem;

namespace Gahame.GameObjects
{
    public class PlayerObjectOverworldSidePerspective : GameObject
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

        bool slowTime = false;

        // Constructor stufferoo for playerino
        public PlayerObjectOverworldSidePerspective(GameScreen screen) : base(screen)
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
            screen.CamController.MovementAmount = new Vector2(.2f, .05f);
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
        }

        // Update stufferino
        public override void Update(GameTime gameTime)
        {

            // Walking left and right
            if (GameInput.RightCD || GameInput.LeftCD)
            {
                sprite.SpriteScale.X = MyMaths.Lerp(sprite.SpriteScale.X, (GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0), .25f * GahameController.GameSpeed * (GameInput.ControllerMode ? Math.Abs(GameInput.AbsLeftStickX) : 1));

                // Approach max speed
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X,
                    maxSpeed * (GameInput.ControllerMode ? GameInput.AbsLeftStickX : 1) * ((GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0)),
                    GahameController.GameSpeed * (physics.Grounded ? accelerationSpeed : airAccelerationSpeed));
            }
            // Stopping
            if (!GameInput.RightCD && !GameInput.LeftCD || GameInput.RightCD && GameInput.LeftCD)
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, GahameController.GameSpeed * (physics.Grounded ? slowDownSpeed : airSlowDownSpeed));

            // Jumping
            if (physics.Grounded)
            {
                if (GameInput.JumpBufferCD) physics.Velocity.Y = -jumpHeight * Math.Sign(Physics.Gravity);
            }
            // Stopping if space is not held
            if (((Physics.Gravity > 0) ? physics.Velocity.Y < 0 : physics.Velocity.Y > 0) && !GameInput.JumpHeld)
                physics.Velocity.Y = (Physics.Gravity > 0) ? Math.Max(physics.Velocity.Y, -minJumpHeight * Math.Sign(Physics.Gravity)) : Math.Min(physics.Velocity.Y, -(jumpHeight / 2) * Math.Sign(Physics.Gravity));

            // Interact with object
            if (GameInput.ActivateCD)
            {
                Dialogue d = hitBox.DialogueMeeting(Position + sprite.SpriteScale);
                if (d != null) d.StartDialogue();
            }

            if (GameInput.ActivateCD) slowTime = !slowTime;
            GahameController.GameSpeed = MyMaths.Lerp(GahameController.GameSpeed, slowTime ? 0.2f : 1, .20f);



            // Updates Components last*/
            base.Update(gameTime);
        }

        // dDrawerino
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Speed test
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.X.ToString()).X / 2, 32), Color.Black);
        }

    }
}

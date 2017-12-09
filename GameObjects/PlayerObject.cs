using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.DialogueSystem;
using Gahame.GameObjects.ObjectComponents.Colliders;

namespace Gahame.GameObjects
{
    public class PlayerObject : GameObject
    {

        // Componenst
        Sprite sprite;
        HitBox hitBox;
        Physics physics;

        // Jump height
        float jumpHeight;

        // Constructor stufferoo for playerino
        public PlayerObject(GameScreen screen) : base(screen)
        {
            // Player specific stuff
            jumpHeight = 5;

            // Cool sprite stuff
            sprite = new Sprite(this);
            sprite.Depth = .1f;
            sprite.AddImage("Sprite");
            sprite.SpriteOrigin = new Vector2(32, 32);
            Components.Add(sprite);

            // HitBox COmponent 
            hitBox = new HitBox(this);
            hitBox.Colliders.Add(new BoxCollider(new Vector2(32, 62)));
            hitBox.Colliders[0].Offset.X = -16;
            hitBox.Colliders[0].Offset.Y = -31;
            Components.Add(hitBox);

            // Physics
            physics = new Physics(this);
            physics.Solid = true;
            physics.GravityEnabled = true;
            Components.Add(physics);

            // Camera
            screen.CamController.target = this;
            screen.CamController.CamOffset.Y = -16;
            screen.CamController.Static = true;
            screen.CamController.SetPosition(new Vector2(330, 100));
        }

        // Update stufferino
        public override void Update(GameTime gameTime)
        {

            // Walking left and right
            if (GameControlls.Right || GameControlls.Left)
            {
                sprite.SpriteScale.X = MyMaths.Lerp(sprite.SpriteScale.X, (GameControlls.Right ? 1 : 0) - (GameControlls.Left ? 1 : 0), .25f * GahameController.GameSpeed);
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 2 * ((GameControlls.Right ? 1 : 0) - (GameControlls.Left ? 1 : 0)), .5f * GahameController.GameSpeed);
            }
            // Stopping
            if (!GameControlls.Right && !GameControlls.Left || GameControlls.Right && GameControlls.Left)
                physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, .25f * GahameController.GameSpeed);
            
            // Jumping
            if (physics.Grounded)
            {
                if (GameControlls.Space) physics.Velocity.Y = -jumpHeight * Math.Sign(Physics.Gravity);
            }
            // Stopping if space is not held
            if (((Physics.Gravity > 0) ? physics.Velocity.Y < 0 : physics.Velocity.Y > 0) && !GameControlls.SpaceHeld)
                physics.Velocity.Y = (Physics.Gravity > 0) ? Math.Max(physics.Velocity.Y, -(jumpHeight/2) * Math.Sign(Physics.Gravity)) : Math.Min(physics.Velocity.Y, -(jumpHeight / 2) * Math.Sign(Physics.Gravity));

            // Interact with object
            if (GameControlls.E){
                Dialogue d = hitBox.DialogueMeeting(Position + sprite.SpriteScale);
                if (d != null) d.StartDialogue();
            }

            // TEST gameSPeed SAK
            if (GameControlls.Up || GameControlls.Down) GahameController.GameSpeed = MyMaths.Lerp(GahameController.GameSpeed, GameControlls.Up ? 1 : 0, .05f);

            // Updates Components last*/
            base.Update(gameTime);
        }

        // dDrawerino
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}

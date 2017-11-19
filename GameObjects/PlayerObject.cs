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

        }

        // Update stufferino
        public override void Update(GameTime gameTime)
        {
            if (GameControlls.Right && !GameControlls.Left)
            {
                sprite.SpriteScale.X = lerpFloat(sprite.SpriteScale.X, 1, .25f);
                physics.Velocity.X = approach(physics.Velocity.X, 2, .5f);
            }
            if (GameControlls.Left && !GameControlls.Right)
            {
                sprite.SpriteScale.X = lerpFloat(sprite.SpriteScale.X, -1, .25f);
                physics.Velocity.X = approach(physics.Velocity.X, -2, .5f);
            }
            if (!GameControlls.Right && !GameControlls.Left || GameControlls.Right && GameControlls.Left)
            {
                physics.Velocity.X = approach(physics.Velocity.X, 0, .25f);
            }
            if (GameControlls.Right && GameControlls.Left) sprite.SpriteScale.X = lerpFloat(sprite.SpriteScale.X, 0, .25f);
            
            if (physics.Grounded)
            {
                if (GameControlls.Space) physics.Velocity.Y = -jumpHeight * signum(Physics.Gravity);
            }
            if (((Physics.Gravity > 0) ? physics.Velocity.Y < 0 : physics.Velocity.Y > 0) && !GameControlls.SpaceHeld)
                physics.Velocity.Y = (Physics.Gravity > 0) ? min(physics.Velocity.Y, -(jumpHeight/5)*2 * signum(Physics.Gravity)) : max(physics.Velocity.Y, -(jumpHeight / 5) * 2 * signum(Physics.Gravity));

            if (GameControlls.E) Physics.Gravity *= -1;

            sprite.SpriteRotation = lerpFloat(sprite.SpriteRotation,(float)(Math.PI * signum(-signum(Physics.Gravity)+1)), .15f);

            // Updates Components last*/
            base.Update(gameTime);
        }

        // Drawerino
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}

using System;

using Microsoft.Xna.Framework;

using Gahame.GameUtils;

namespace Gahame.GameObjects.ObjectComponents
{
    public class Physics : ObjectComponent
    {

        // gravity
        public static float Gravity = 0.25f;

        // Gravity enabled
        public bool GravityEnabled; 

        // Check if object will colide with wall
        public bool Solid;

        // Velocity vector
        public Vector2 Velocity;

        // Grounded
        public bool Grounded;

        // Constructor
        public Physics(GameObject gameObject) : base(gameObject)
        {
            // Fixes important things
            Updatable = true;
            Drawable = false;
            this.gameObject = gameObject;

            // variables that only affect physics stuff
            Solid = false;
            GravityEnabled = false;
            Velocity = new Vector2(0, 0);
            Grounded = false;
        }

        // Updates physics
        public override void Update(GameTime gameTime)
        {
            HitBox hb = gameObject.GetComponent<HitBox>();
            if (Solid && hb != null)
            {

                // Cool gravity memes
                if (GravityEnabled)
                {
                    if (!hb.SolidMeeting(gameObject.Position.X, gameObject.Position.Y + Math.Sign(Gravity)))
                    {
                        Grounded = false;
                        Velocity.Y += Gravity * GahameController.GameSpeed;
                    }
                    else Grounded = true;
                }

                // Horizontal collision
                if (hb.SolidMeeting(gameObject.Position.X + Velocity.X * GahameController.GameSpeed, gameObject.Position.Y))
                {
                    gameObject.Position.X = (Velocity.X > 0) ? (int)gameObject.Position.X : (int)gameObject.Position.X + 1; 
                    while (!hb.SolidMeeting(gameObject.Position.X + Math.Sign(Velocity.X), gameObject.Position.Y))
                    {
                        gameObject.Position.X += Math.Sign(Velocity.X);
                    }
                    Velocity.X = 0;
                }
                gameObject.Position.X += Velocity.X * GahameController.GameSpeed; // Updates x position

                // Vertical collision
                if (hb.SolidMeeting(gameObject.Position.X, gameObject.Position.Y + Velocity.Y * GahameController.GameSpeed))
                {
                    gameObject.Position.Y = (Velocity.Y > 0) ? (int)gameObject.Position.Y : (int)gameObject.Position.Y + 1;
                    while (!hb.SolidMeeting(gameObject.Position.X, gameObject.Position.Y + Math.Sign(Velocity.Y)))
                    {
                        gameObject.Position.Y += Math.Sign(Velocity.Y);
                    }
                    Velocity.Y = 0;
                }
                gameObject.Position.Y += Velocity.Y * GahameController.GameSpeed; // Update Y position

            } else
            {
                if (GravityEnabled) Velocity.Y += Gravity;
                gameObject.Position.X += Velocity.X * GahameController.GameSpeed;
                gameObject.Position.Y += Velocity.Y * GahameController.GameSpeed;
            }
        }

        // Moves in direction
        public void MoveInDirection(float velocity, float angle)
        {
            // Calculate stuff
            Velocity.Y = (float)(Math.Sin(angle) * velocity);
            Velocity.X = (float)(Math.Cos(angle) * velocity);
        }

    }
}

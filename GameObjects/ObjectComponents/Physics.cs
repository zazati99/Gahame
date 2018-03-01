using System;

using Microsoft.Xna.Framework;

using Gahame.GameUtils;
using Gahame.GameObjects.ObjectComponents.Colliders;

namespace Gahame.GameObjects.ObjectComponents
{
    public class Physics : ObjectComponent
    {
        // gravity
        public static Vector2 Gravity =  new Vector2(0, 0.25f);

        // Gravity enabled
        public bool GravityEnabled; 

        // Check if object will colide with wall
        public bool Solid;

        // will it inherit velocity hmmm?
        public bool InheritVelocity;

        // Velocity vector
        public Vector2 Velocity;

        // Grounded
        public bool Grounded;

        // Constructor
        public Physics(GameObject gameObject) : base(gameObject)
        {
            // Fixes important things
            this.gameObject = gameObject;

            // variables that only affect physics stuff
            Solid = false;
            GravityEnabled = false;
            Velocity = new Vector2(0, 0);
            Grounded = false;
            InheritVelocity = false;
        }

        // Updates physics
        public override void Update(GameTime gameTime)
        {
            // Checks if HitBox Exisits
            HitBox hb = gameObject.GetComponent<HitBox>();
            if (Solid && hb != null)
            {
                // Cool gravity memes
                if (GravityEnabled)
                {
                    if (!hb.SolidMeeting(gameObject.Position.X + Gravity.X, gameObject.Position.Y + Gravity.Y))
                    {
                        Grounded = false;
                        Velocity += Gravity * GahameController.GameSpeed;
                    }
                    else
                    {
                        Grounded = true;
                        if (InheritVelocity){
                            HitBox solid = hb.SolidPlace(new Vector2(gameObject.Position.X + Gravity.X, gameObject.Position.Y + Gravity.Y));
                            if (solid.gameObject.GetComponent<Physics>() is Physics p)
                            {
                                Velocity = p.Velocity;
                            }   
                        }
                    }
                }

                // Horizontal collision (Advanced stuff)
                if (hb.SolidMeeting(gameObject.Position.X + Velocity.X * GahameController.GameSpeed, gameObject.Position.Y))
                {
                    HitBox otherSolid = hb.SolidPlace(new Vector2(gameObject.Position.X + Velocity.X * GahameController.GameSpeed, gameObject.Position.Y));
                    if (hb.Priority > otherSolid.Priority)
                    {
                        //otherSolid.gameObject.Position.X += Velocity.X;
                        float newPos = (gameObject.Position.X + Velocity.X - otherSolid.gameObject.Position.X) - gameObject.Position.X + otherSolid.gameObject.Position.X;
                        otherSolid.gameObject.Position.X += newPos;
                    }
                    else
                    {
                        gameObject.Position.X = (Velocity.X > 0) ? (int)gameObject.Position.X : (int)gameObject.Position.X + 1;
                        while (!hb.SolidMeeting(gameObject.Position.X + Math.Sign(Velocity.X), gameObject.Position.Y))
                        {
                            gameObject.Position.X += Math.Sign(Velocity.X);
                        }
                        Velocity.X = 0;
                    }
                }
                gameObject.Position.X += Velocity.X * GahameController.GameSpeed; // Updates x position

                // Vertical collision (Advanced stuff)
                if (hb.SolidMeeting(gameObject.Position.X, gameObject.Position.Y + Velocity.Y * GahameController.GameSpeed))
                {
                    HitBox otherSolid = hb.SolidPlace(new Vector2(gameObject.Position.X, gameObject.Position.Y + Velocity.Y * GahameController.GameSpeed));
                    if (hb.Priority > otherSolid.Priority)
                    {
                        float newPos = (gameObject.Position.Y + Velocity.Y - otherSolid.gameObject.Position.Y) - gameObject.Position.Y + otherSolid.gameObject.Position.Y;
                        otherSolid.gameObject.Position.Y += newPos;
                    }
                    else
                    {
                        gameObject.Position.Y = (Velocity.Y > 0) ? (int)gameObject.Position.Y : (int)gameObject.Position.Y + 1;
                        while (!hb.SolidMeeting(gameObject.Position.X, gameObject.Position.Y + Math.Sign(Velocity.Y)))
                        {
                            gameObject.Position.Y += Math.Sign(Velocity.Y);
                        }
                        Velocity.Y = 0;
                    }
                }
                gameObject.Position.Y += Velocity.Y * GahameController.GameSpeed; // Update Y position
            }
            else // Just do velocity stuff without checking for collisions
            {
                if (GravityEnabled) Velocity += Gravity * GahameController.GameSpeed;
                gameObject.Position += Velocity * GahameController.GameSpeed;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Gahame.GameObjects;

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

        // Type that collision checks will be against
        public Type colType;

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

            // Type
            colType = typeof(WallObject);
        }

        // Updates physics
        public override void Update(GameTime gameTime)
        {
            if (Solid)
            {

                // Cool gravity memes
                if (GravityEnabled)
                {
                    if (!gameObject.PlaceMeeting(gameObject.Position.X, gameObject.Position.Y + signum(Gravity), colType))
                    {
                        Grounded = false;
                        Velocity.Y += Gravity;
                    }
                    else Grounded = true;
                }

                // Horizontal collision
                if (gameObject.PlaceMeeting(gameObject.Position.X + Velocity.X, gameObject.Position.Y, colType))
                {
                    gameObject.Position.X = (int)(gameObject.Position.X - Velocity.X);
                    while (!gameObject.PlaceMeeting(gameObject.Position.X + signum(Velocity.X), gameObject.Position.Y, colType))
                    {
                        gameObject.Position.X += signum(Velocity.X);
                    }
                    Velocity.X = 0;
                }
                gameObject.Position.X += Velocity.X; // Updates x position

                // Vertical collision
                if (gameObject.PlaceMeeting(gameObject.Position.X, gameObject.Position.Y + Velocity.Y, colType))
                {
                    gameObject.Position.Y = (int)(gameObject.Position.Y - Velocity.Y);
                    while (!gameObject.PlaceMeeting(gameObject.Position.X, gameObject.Position.Y + signum(Velocity.Y), colType))
                    {
                        gameObject.Position.Y += signum(Velocity.Y);
                    }
                    Velocity.Y = 0;
                }
                gameObject.Position.Y += Velocity.Y; // Update Y position

            } else
            {
                if (GravityEnabled) Velocity.Y += Gravity;
                gameObject.Position.X += Velocity.X;
                gameObject.Position.Y += Velocity.Y;
            }

        }

        int signum(float value)
        {
            return (value == 0) ? 0 : (value > 0) ? 1 : -1;
        }

    }
}

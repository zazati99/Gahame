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
            if (Solid)
            {
                // Horizontal collision
                if (gameObject.PlaceMeeting<WallObject>(gameObject.Position.X + Velocity.X, gameObject.Position.Y))
                {
                    gameObject.Position.X = (int)(gameObject.Position.X - Velocity.X);
                    while (!gameObject.PlaceMeeting<WallObject>(gameObject.Position.X + signum(Velocity.X), gameObject.Position.Y))
                    {
                        gameObject.Position.X += signum(Velocity.X);
                    }
                    Velocity.X = 0;
                } gameObject.Position.X += Velocity.X; // Updates x position

                // Vertical collision
                if (gameObject.PlaceMeeting<WallObject>(gameObject.Position.X, gameObject.Position.Y + Velocity.Y))
                {
                    gameObject.Position.Y = (int)(gameObject.Position.Y - Velocity.Y);
                    while (!gameObject.PlaceMeeting<WallObject>(gameObject.Position.X, gameObject.Position.Y + signum(Velocity.Y)))
                    {
                        gameObject.Position.Y += signum(Velocity.Y);
                    }
                    Velocity.Y = 0;
                }

                // Cool gravity memes
                if (GravityEnabled)
                {
                    if (!gameObject.PlaceMeeting<WallObject>(gameObject.Position.X, gameObject.Position.Y + 1))
                    {
                        Grounded = false;
                        Velocity.Y += Gravity;
                    }
                    else Grounded = true; 
                }
                // Update Y position
                gameObject.Position.Y += Velocity.Y;

            } else
            {
                if (GravityEnabled) Velocity.Y += Gravity;
                gameObject.Position.X += Velocity.X;
                gameObject.Position.Y += Velocity.Y;
            }

        }

        int signum(float value)
        {
            if (value > 0) return 1;
            if (value < 0) return -1;
            return 0;
        }

    }
}

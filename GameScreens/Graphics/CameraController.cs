using Gahame.GameObjects;
using Gahame.GameUtils;

using Microsoft.Xna.Framework;

namespace Gahame.GameScreens
{
    // Controlls the camera every GameScreen should use this
    public class CameraController
    {
        // Position of camera in room
        public Vector2 Position;

        // Rotation of camera in room
        public float Rotation;

        // target of camer, will move towards this
        public GameObject Target;

        // How fast it will move towards target
        public Vector2 MovementAmount;

        // Constructor
        public CameraController()
        {
            // default position of camera is 0
            Position = Vector2.Zero;

            // No default target
            Target = null;

            // default move ammount is 1
            MovementAmount = Vector2.One;
        }

        // Updates camera
        public void Update()
        {
            // Do stuff with the target position
            if (Target != null)
            {
                Position.X += (Target.Position.X - Position.X) * MovementAmount.X * (1 + GahameController.GameSpeed) / 2;
                Position.Y += (Target.Position.Y - Position.Y) * MovementAmount.Y * (1 + GahameController.GameSpeed) / 2;

                // Stops X position from going outside of screens
                if (Position.X < -Camera.ViewOffset.X)
                {
                    Position.X = -Camera.ViewOffset.X;
                } else if (Position.X > Target.screen.ScreenSize.X + Camera.ViewOffset.X)
                {
                    Position.X = Target.screen.ScreenSize.X + Camera.ViewOffset.X;
                }
                // Stops Y position from going outside of screens
                if (Position.Y < -Camera.ViewOffset.Y)
                {
                    Position.Y = -Camera.ViewOffset.Y;
                }
                else if (Position.Y > Target.screen.ScreenSize.Y + Camera.ViewOffset.Y)
                {
                    Position.Y = Target.screen.ScreenSize.Y + Camera.ViewOffset.Y;
                }
            }

            // Set position of camera to this position
            Camera.SetPosition(Position);
            Camera.SetRotation(Rotation);
        }
    }
}

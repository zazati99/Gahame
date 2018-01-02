using Gahame.GameUtils;
using Gahame.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Gahame.GameScreens
{
    public class Camera
    {
        // Camera variables
        // Position of camera
        public static Vector2 Position { get; private set; }
        public static Vector2 ViewOffset;

        // what you will be able to see
        public static Vector2 View;
        Vector2 defaultView;

        // Port on screen
        public Vector2 Port;
        Vector2 defaultPort;

        // Rotation of camera
        public static float Rotation { get; private set; }

        // Graphics that will be needed to change some stuff
        GraphicsDeviceManager graphics;
        
        // Constructor
        public Camera(Vector2 defaultPort, Vector2 defaultView, GraphicsDeviceManager graphics)
        {
            // Rotation
            Rotation = 0;

            // Graphics device manager
            this.graphics = graphics;

            // set default position to 0
            Position = Vector2.Zero;

            // set the port
            this.defaultPort = defaultPort;
            SetPort(defaultPort);

            // view will be port multiplied divided by 2
            this.defaultView = defaultView;
            View = defaultView;
            ViewOffset = new Vector2(-View.X / 2, -View.Y / 2);
        }

        // Sets the port
        public void SetPort(Vector2 port)
        {
            // Sets port
            Port = port;

            // Fix the window size
            graphics.PreferredBackBufferWidth = (int)defaultPort.X;
            graphics.PreferredBackBufferHeight = (int)defaultPort.Y;
        }

        // Switch fullscreen
        public void SwitchFullscreen()
        {
            // switches fullscreen and apply
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();

            // Fixes viewport if it's fullscreen
            if (graphics.IsFullScreen)
            {
                // Get screen resolution
                float screenResX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                float screenResY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                // sets the port to screen resolution
                SetPort(new Vector2(screenResX, screenResY));

                // make sure that aspect ratio is correct and fix offset
                View.Y = View.X * screenResY / screenResX;
                ViewOffset.Y = -View.Y / 2;

            }
            // Go back to default port otherwise
            else
            {
                // sets default values
                SetPort(defaultPort);
                View = defaultView;
                ViewOffset.Y = -View.Y / 2;
            }
        }

        // Transformation, this is what will get passed in to camera
        public Matrix GetTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                   Matrix.CreateScale(new Vector3(Port.X / View.X, Port.Y / View.Y, 0)) *
                   Matrix.CreateRotationZ(Rotation) * 
                   Matrix.CreateTranslation(new Vector3(Port/2, 0));
        }
        public Matrix UiTransformation()
        {
            return Matrix.CreateScale(new Vector3(Port.X / View.X, Port.Y / View.Y, 0));
        }

        // Set rotation
        public static void SetRotation(float rotation)
        {
            Rotation = rotation;
        }

        // Set Position;
        public static void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        // Get position
        public static Vector2 GetPosition()
        {
            return Position;
        }

        // Gets specified position on screen
        public static Vector2 PositionOnScreen(Vector2 pos)
        {
            return Position + pos + ViewOffset;
        }
    }
}

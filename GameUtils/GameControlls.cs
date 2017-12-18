﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gahame.GameUtils
{
    public class GameControlls
    {

        // Previous keyboard and gamepad state
        static KeyboardState previousKeyboardState = Keyboard.GetState();
        static GamePadState previousGamePadState = GamePad.GetState(0);

        // Specified controlls
        public static bool Right { get; private set; }
        public static bool Left { get; private set; }
        public static bool Up { get; private set; }
        public static bool Down { get; private set; }

        public static bool RightPressed { get; private set; }
        public static bool LeftPressed { get; private set; }
        public static bool UpPressed { get; private set; }
        public static bool DownPressed { get; private set; }

        public static bool Activate { get; private set; }

        public static bool RightCD { get; private set; }
        public static bool LeftCD { get; private set; }
        public static bool UpCD { get; private set; }
        public static bool DownCD { get; private set; }

        public static bool JumpCD { get; private set; }
        public static bool ActivateCD { get; private set; }

        public static bool Jump { get; private set; }
        public static bool JumpHeld { get; private set; }

        public static bool Esc { get; private set; }
        public static bool Enter { get; private set; }
        public static bool F5 { get; private set; }
        public static bool F6 { get; private set; }

        public static bool JumpBufferCD { get; private set; }
        private static int jumpBuffer;

        public static float AbsLeftStickX { get; private set; }
        public static float AbsLeftStickY { get; private set; }

        // check if last used controlls was controller if false it is keyboard
        public static bool ControllerMode { get; private set; }

        // Checks all of the controlls (should happen in Game class) 
        public static void Update() 
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);

            // Check is keyboard mode or controller mode 
            if (gamePadState.IsConnected)
            {
                // Get buttons pressed
                bool keyboardButtonPressed = keyboardState.GetPressedKeys().Length > 0;
                GamePadState emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, 0);
                // checks the keyboard and controller buttons
                if (gamePadState != emptyInput && !keyboardButtonPressed)
                {
                    ControllerMode = true;
                }
                else if (keyboardButtonPressed) ControllerMode = false;
            }
            else ControllerMode = false;

            // left controller stick on X axis
            AbsLeftStickX = Math.Min(Math.Abs(gamePadState.ThumbSticks.Left.X) + .10f, 1);
            // Left controller stick on Y axis
            AbsLeftStickY = Math.Min(Math.Abs(gamePadState.ThumbSticks.Left.Y) + .10f, 1);

            Right = keyboardState.IsKeyDown(Keys.D) || gamePadState.ThumbSticks.Left.X > .20f;
            Left = keyboardState.IsKeyDown(Keys.A) || gamePadState.ThumbSticks.Left.X < -.20f;
            Up = keyboardState.IsKeyDown(Keys.W) || gamePadState.ThumbSticks.Left.Y > .20f;
            Down = keyboardState.IsKeyDown(Keys.S) || gamePadState.ThumbSticks.Left.Y < -.20f;

            RightPressed = (keyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)) || (gamePadState.ThumbSticks.Left.X > .20f && !(previousGamePadState.ThumbSticks.Left.X > .20f));
            LeftPressed = (keyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)) || (gamePadState.ThumbSticks.Left.X < -.20f && !(previousGamePadState.ThumbSticks.Left.X < -.20f));
            UpPressed = (keyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)) || (gamePadState.ThumbSticks.Left.Y > .20f && !(previousGamePadState.ThumbSticks.Left.Y > .20f));
            DownPressed = (keyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)) || (gamePadState.ThumbSticks.Left.Y < -.20f && !(previousGamePadState.ThumbSticks.Left.Y < -.20f));

            Jump = (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) || (gamePadState.IsButtonDown(Buttons.A) && !previousGamePadState.IsButtonDown(Buttons.A));
            JumpHeld = keyboardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A);

            Activate = (keyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E)) || (gamePadState.IsButtonDown(Buttons.B) && !previousGamePadState.IsButtonDown(Buttons.B));

            Esc = (keyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) || (gamePadState.IsButtonDown(Buttons.Start) && !previousGamePadState.IsButtonDown(Buttons.Start));
            Enter = keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter) || (gamePadState.IsButtonDown(Buttons.Back) && !previousGamePadState.IsButtonDown(Buttons.Back));

            F5 = keyboardState.IsKeyDown(Keys.F5) && !previousKeyboardState.IsKeyDown(Keys.F5);
            F6 = keyboardState.IsKeyDown(Keys.F6) && !previousKeyboardState.IsKeyDown(Keys.F6);

            RightCD = Right && !GahameController.CutScene;
            LeftCD = Left && !GahameController.CutScene;
            UpCD = Up && !GahameController.CutScene;
            DownCD = Down && !GahameController.CutScene;

            JumpCD = Jump && !GahameController.CutScene;
            ActivateCD = Activate && !GahameController.CutScene;

            // Space buffer for jumping
            if (jumpBuffer > 0) jumpBuffer--;
            if (JumpCD) jumpBuffer = 3;
            JumpBufferCD = (jumpBuffer > 0);

            previousKeyboardState = keyboardState;
            previousGamePadState = gamePadState;
        }

    }
}

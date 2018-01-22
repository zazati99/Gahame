using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gahame.GameUtils
{
    public class GameInput
    {

        // Previous keyboard and gamepad state
        static KeyboardState previousKeyboardState = Keyboard.GetState();
        static GamePadState previousGamePadState = GamePad.GetState(0);

        // Current Keyboard state
        static KeyboardState keyboardState;
        static GamePadState gamePadState;

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
        public static bool EscHeld { get; private set; }
        public static bool Enter { get; private set; }
        public static bool F5 { get; private set; }
        public static bool F6 { get; private set; }
        public static bool Shift { get; private set; }

        public static bool DeletePressed { get; private set; }

        public static bool JumpBufferCD { get; private set; }
        private static int jumpBuffer;

        public static float AbsLeftStickX { get; private set; }
        public static float AbsLeftStickY { get; private set; }
        public static float LeftStickX { get; private set; }
        public static float LeftStickY { get; private set; }

        // check if last used controlls was controller if false it is keyboard
        public static bool ControllerMode { get; private set; }

        // Stick deadzone
        private static float stickDeadZone = .125f;

        // Checks all of the controlls (should happen in Game class) 
        public static void Update() 
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(0);

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

            // Abs left controller stick on X axis
            AbsLeftStickX = Math.Min(Math.Abs(gamePadState.ThumbSticks.Left.X) + .05f, 1);
            // Abs left controller stick on Y axis
            AbsLeftStickY = Math.Min(Math.Abs(gamePadState.ThumbSticks.Left.Y) + .05f, 1);

            // left controller stick on X axis
            LeftStickX = MyMaths.Clamp(gamePadState.ThumbSticks.Left.X + Math.Sign(gamePadState.ThumbSticks.Left.X) * .05f, -1, 1);
            // Left controller stick on Y axis
            LeftStickY = -MyMaths.Clamp(gamePadState.ThumbSticks.Left.Y + Math.Sign(gamePadState.ThumbSticks.Left.Y) * .05f, -1, 1);

            Right = keyboardState.IsKeyDown(Keys.D) || gamePadState.ThumbSticks.Left.X > stickDeadZone;
            Left = keyboardState.IsKeyDown(Keys.A) || gamePadState.ThumbSticks.Left.X < -stickDeadZone;
            Up = keyboardState.IsKeyDown(Keys.W) || gamePadState.ThumbSticks.Left.Y > stickDeadZone;
            Down = keyboardState.IsKeyDown(Keys.S) || gamePadState.ThumbSticks.Left.Y < -stickDeadZone;

            RightPressed = (keyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)) || (gamePadState.ThumbSticks.Left.X > stickDeadZone && !(previousGamePadState.ThumbSticks.Left.X > stickDeadZone));
            LeftPressed = (keyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)) || (gamePadState.ThumbSticks.Left.X < -stickDeadZone && !(previousGamePadState.ThumbSticks.Left.X < -stickDeadZone));
            UpPressed = (keyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)) || (gamePadState.ThumbSticks.Left.Y > stickDeadZone && !(previousGamePadState.ThumbSticks.Left.Y > stickDeadZone));
            DownPressed = (keyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)) || (gamePadState.ThumbSticks.Left.Y < -stickDeadZone && !(previousGamePadState.ThumbSticks.Left.Y < -stickDeadZone));

            Jump = (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) || (gamePadState.IsButtonDown(Buttons.A) && !previousGamePadState.IsButtonDown(Buttons.A));
            JumpHeld = keyboardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A);

            Activate = (keyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E)) || (gamePadState.IsButtonDown(Buttons.B) && !previousGamePadState.IsButtonDown(Buttons.B));

            Esc = (keyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) || (gamePadState.IsButtonDown(Buttons.Start) && !previousGamePadState.IsButtonDown(Buttons.Start));
            EscHeld = (keyboardState.IsKeyDown(Keys.Escape) || gamePadState.IsButtonDown(Buttons.Start));

            Enter = keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter) || (gamePadState.IsButtonDown(Buttons.Back) && !previousGamePadState.IsButtonDown(Buttons.Back));

            DeletePressed = keyboardState.IsKeyDown(Keys.Delete) && !previousKeyboardState.IsKeyDown(Keys.Delete);

            F5 = keyboardState.IsKeyDown(Keys.F5) && !previousKeyboardState.IsKeyDown(Keys.F5);
            F6 = keyboardState.IsKeyDown(Keys.F6) && !previousKeyboardState.IsKeyDown(Keys.F6);

            Shift = keyboardState.IsKeyDown(Keys.LeftShift);

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
        }

        public static void EndUpdate()
        {
            previousKeyboardState = keyboardState;
            previousGamePadState = gamePadState;
        }

        // Get keyboard input and return a char
        public static void AddInputToString(ref String str)
        {
            // Get the keys that is currently pressed
            Keys[] keys = keyboardState.GetPressedKeys();

            // Loop through pressed keys an meme a bit
            for (int i = 0; i < keys.Length; i++)
            {
                if (!previousKeyboardState.IsKeyDown(keys[i]))
                {
                    if (keys[i] != Keys.Enter && keys[i] != Keys.Space && keys[i] != Keys.Back)
                    {
                        str += keys[i].ToString();

                    } else if (keys[i] == Keys.Space)
                    {
                        str += " ";
                    } else if (keys[i] == Keys.Back)
                    {
                        if (str.Length > 0)
                            str = str.Remove(str.Length - 1);
                    }
                }
            }
        }
    }
}

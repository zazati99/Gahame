using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Gahame.GameUtils
{
    public class GameControlls
    {

        // Previous keyboard state
        static KeyboardState previousState = Keyboard.GetState();

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

        public static bool SpaceCD { get; private set; }
        public static bool ActivateCD { get; private set; }

        public static bool E { get; private set; }
        public static bool Space { get; private set; }
        public static bool SpaceHeld { get; private set; }
        public static bool Esc { get; private set; }
        public static bool Enter { get; private set; }
        public static bool F5 { get; private set; }
        public static bool F6 { get; private set; }

        // Checks all of the controlls (should happen in Game class) 
        public static void Update() 
        {
            KeyboardState state = Keyboard.GetState();

            Right = state.IsKeyDown(Keys.D);
            Left = state.IsKeyDown(Keys.A);
            Up = state.IsKeyDown(Keys.W);
            Down = state.IsKeyDown(Keys.S);

            RightPressed = state.IsKeyDown(Keys.D) && !previousState.IsKeyDown(Keys.D);
            LeftPressed = state.IsKeyDown(Keys.A) && !previousState.IsKeyDown(Keys.A);
            UpPressed = state.IsKeyDown(Keys.W) && !previousState.IsKeyDown(Keys.W);
            DownPressed = state.IsKeyDown(Keys.S) && !previousState.IsKeyDown(Keys.S);

            SpaceHeld = state.IsKeyDown(Keys.Space);

            E = state.IsKeyDown(Keys.E) && !previousState.IsKeyDown(Keys.E);
            Space = state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space);
            Esc = state.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape);
            Enter = state.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter);
            F5 = state.IsKeyDown(Keys.F5) && !previousState.IsKeyDown(Keys.F5);
            F6 = state.IsKeyDown(Keys.F6) && !previousState.IsKeyDown(Keys.F6);

            RightCD = Right && !GahameController.CutScene;
            LeftCD = Left && !GahameController.CutScene;
            UpCD = Up && !GahameController.CutScene;
            DownCD = Down && !GahameController.CutScene;

            SpaceCD = Space && !GahameController.CutScene;
            ActivateCD = E && !GahameController.CutScene;

            previousState = state;
        }

    }
}

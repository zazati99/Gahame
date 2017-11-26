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

            SpaceHeld = state.IsKeyDown(Keys.Space);

            E = state.IsKeyDown(Keys.E) && !previousState.IsKeyDown(Keys.E);
            Space = state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space);
            Esc = state.IsKeyDown(Keys.Escape) && !previousState.IsKeyDown(Keys.Escape);
            Enter = state.IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter);
            F5 = state.IsKeyDown(Keys.F5) && !previousState.IsKeyDown(Keys.F5);
            F6 = state.IsKeyDown(Keys.F6) && !previousState.IsKeyDown(Keys.F6);

            previousState = state;
        }

    }
}

using Microsoft.Xna.Framework.Input;

namespace Gahame.GameUtils
{
    public class Input
    {
        // KEY and buttons 
        public Keys key;
        public Buttons button;

        // Constructor town
        public Input()
        {

        }
        public Input(Keys key)
        {
            this.key = key;
        }
        public Input(Keys key, Buttons button)
        {
            this.key = key;
            this.button = button;
        }
    }
}

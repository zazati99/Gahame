using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Gahame.GameUtils
{
    public class GameFonts
    {
        // Fonts
        public static SpriteFont Arial { get; private set; }
        public static GameFont GahameFont { get; private set; }

        // Load the fonts
        public static void LoadFonts(ContentManager content)
        {
            Arial = content.Load<SpriteFont>("Fonts/DialogueArial");

            GahameFont = new GameFont();
            GahameFont.LoadFont(content, "Fonts/GahameFont");
        }
    }
}

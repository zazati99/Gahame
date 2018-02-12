using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameUtils
{
    public class TextRenderer
    {

        // consonants
        public static char[] cons = { 'M', 'D', 'B', 'K', 'N', 'R', 'P', 'W', 'F', 'H', 'S', 'C', 'L', 'J' };

        // vowels
        public static char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Å', 'Y', 'Ä' };

#region Text drawing methods

        // Draw mixed text
        public static void DrawMixedText(SpriteBatch spriteBatch, SpriteFont font, GameFont font2, string s, Vector2 pos, Color color)
        {

            

        }

#endregion

        // Check if a character is a consonant
        public static bool isCons(char c)
        {
            for (int i = 0; i < cons.Length; i++)
            {
                if (c == cons[i]) return true;
            }
            return false;
        }

        // Check if a character is a vowel
        public static bool isVowel(char c)
        {
            for (int i = 0; i < vowels.Length; i++)
            {
                if (c == vowels[i]) return true;
            }
            return false;
        }

    }
}

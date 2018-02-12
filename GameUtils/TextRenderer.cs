using System;
using System.Linq;

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
        public static void DrawMixedText(SpriteBatch spriteBatch, SpriteFont font, GameFont font2, bool startNormal, string s, Vector2 pos, Color color)
        {
            // Get list of stringw with separator
            string[] subStrings = s.Split('^');

            // The current type of font that will be used
            bool currentTextType = startNormal;

            // The offset of the text
            Vector2 offset = new Vector2(0, 0);

            // Constant offset that will be used by gahame font
            Vector2 gahameFontOffset = new Vector2(1, font2.Size.Y - 8);

            // Draw each substring
            for (int i = 0; i < subStrings.Length; i++)
            {
                // Get each line in substring
                string[] lines = subStrings[i].Split('\n');
       
                // Draw normal text
                if (currentTextType)
                {
                    // Draw each line;
                    for (int j = 0; j < lines.Length; j++)
                    {
                        // Draw text contained in line
                        spriteBatch.DrawString(font, lines[j], pos + offset, color);

                        // Change offset as long as it isn't the last line
                        if (j != lines.Length - 1)
                        {
                            offset.Y += font2.LineSpacing;
                            offset.X = 0;
                        }
                    }

                    // Change X offset to the size of the last line
                    offset.X += font.MeasureString(lines[lines.Length - 1]).X;
                }
                // Draw text using gahame font
                else
                {
                    // Draw each line;
                    for (int j = 0; j < lines.Length; j++)
                    {
                        // Draw text contained in line
                        font2.DrawString(spriteBatch, lines[j], pos + offset + gahameFontOffset, color);

                        // Change offset as long as it isn't the last line
                        if (j != lines.Length - 1)
                        {
                            offset.Y += font2.LineSpacing;
                            offset.X = 0;
                        }
                    }

                    // Change X offset to the size of the last line
                    offset.X += font2.MeasureString(lines[lines.Length - 1]).X;
                }

                // change to other text type
                currentTextType = !currentTextType;
            }
        }

        // Draw mixed text
        public static void DrawMixedShakyText(SpriteBatch spriteBatch, SpriteFont font, GameFont font2, bool startNormal, string s, Vector2 pos, float intensity, Color color)
        {
            // Get list of stringw with separator
            string[] subStrings = s.Split('^');

            // The current type of font that will be used
            bool currentTextType = startNormal;

            // The offset of the text
            Vector2 offset = new Vector2(0, 0);

            // Constant offset that will be used by gahame font
            Vector2 gahameFontOffset = new Vector2(1, font2.Size.Y - 8);

            // Draw each substring
            for (int i = 0; i < subStrings.Length; i++)
            {
                // Get each line in substring
                string[] lines = subStrings[i].Split('\n');

                // Draw normal text
                if (currentTextType)
                {
                    // Draw each line;
                    for (int j = 0; j < lines.Length; j++)
                    {
                        // Draw shaky text contained in line
                        DrawShakingText(spriteBatch, font, lines[j], pos + offset, intensity, color);

                        // Change offset as long as it isn't the last line
                        if (j != lines.Length - 1)
                        {
                            offset.Y += font2.LineSpacing;
                            offset.X = 0;
                        }
                    }

                    // Change X offset to the size of the last line
                    offset.X += font.MeasureString(lines[lines.Length - 1]).X;
                }
                // Draw text using gahame font
                else
                {
                    // Draw each line;
                    for (int j = 0; j < lines.Length; j++)
                    {
                        // Draw shaky text contained in line
                        DrawShakingText(spriteBatch, font2, lines[j], pos + offset + gahameFontOffset, intensity, color);

                        // Change offset as long as it isn't the last line
                        if (j != lines.Length - 1)
                        {
                            offset.Y += font2.LineSpacing;
                            offset.X = 0;
                        }
                    }

                    // Change X offset to the size of the last line
                    offset.X += font2.MeasureString(lines[lines.Length - 1]).X;
                }

                // change to other text type
                currentTextType = !currentTextType;
            }
        }

        // Draw shaky text
        public static void DrawShakingText(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 pos, float intensity, Color c)
        {
            // the offset for each char
            Vector2 offset = new Vector2(0, 0);

            // draw each char
            for (int i = 0; i < text.Length; i++)
            {
                Vector2 shake = new Vector2(MyMaths.RandomInRange(-intensity, intensity), MyMaths.RandomInRange(-intensity, intensity));
                spriteBatch.DrawString(font, ""+text[i], pos + offset + shake, c);

                offset.X += font.MeasureString("" + text[i]).X;

                if (text[i] == '\n')
                {
                    offset.X = 0;
                    offset.Y += font.LineSpacing;
                }
            }
        }

        // Draw shaky gahame text
        public static void DrawShakingText(SpriteBatch spriteBatch, GameFont font, string text, Vector2 pos, float intensity, Color c)
        {
            // the offset for each char
            Vector2 offset = new Vector2(0, 0);

            // Gahamefy it
            text = Gahamefy(text);

            // draw each char
            for (int i = 0; i < text.Length; i++)
            {
                Vector2 shake = new Vector2(MyMaths.RandomInRange(-intensity, intensity), MyMaths.RandomInRange(-intensity, intensity));

                string textToDraw;
                if (IsCons(text[i]))
                {
                    if (i != text.Length - 1)
                    {
                        if (IsVowel(text[i + 1]))
                        {
                            textToDraw = "" + text[i] + text[i + 1];
                            i++;
                        }
                        else textToDraw = "" + text[i];
                    }
                    else textToDraw = "" + text[i];
                }
                else
                {
                    textToDraw = "" + text[i];
                }

                font.DrawString(spriteBatch, textToDraw, pos + offset + shake, c);

                offset.X += font.MeasureString(textToDraw).X;

                if (text[i] == '\n')
                {
                    offset.X = 0;
                    offset.Y += font.LineSpacing;
                }
            }
        }

        #endregion

        #region Other important functions

        // Check if a character is a consonant
        public static bool IsCons(char c)
        {
            return cons.Contains(c);
        }

        // Check if a character is a vowel
        public static bool IsVowel(char c)
        {
            return vowels.Contains(c);
        }

        // Make it readable by gahame font
        public static string Gahamefy(string s)
        {
            string newString = s.ToUpper();
            newString = newString.Replace('T', 'D');
            newString = newString.Replace('G', 'K');
            newString = newString.Replace('V', 'W');

            return newString;
        }

        #endregion

    }
}

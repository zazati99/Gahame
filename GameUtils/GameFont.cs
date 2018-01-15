using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;

namespace Gahame.GameUtils
{
    public class GameFont
    {
        // Texture that holds the font
        Texture2D fontTexture;

        // Rectangles that holds characters
        Dictionary<char, Rectangle> characters;

        // Line spacing
        public float LineSpacing;

        // Character spacing
        public float CharSpacing;

        // consonants
        public static char[] cons = { 'M','D','B','K','N','R','P','W','F','H','S','C','L','J' };

        // vowels
        public static char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Å', 'Y', 'Ä' };

        // Point for width and height
        Point size;

        // Loads texture and memes alot
        public void LoadFont(ContentManager content, string path)
        {
            // Loads from path and memes a bit
            fontTexture = content.Load<Texture2D>(path);
            size.X = fontTexture.Width / 8;
            size.Y = fontTexture.Height / 3;

            // sets default spacing
            LineSpacing = size.Y + 1;
            CharSpacing = size.X + 2;

            // Creates dictionary
            characters = new Dictionary<char, Rectangle>();

            // Sets consonants
            Point charPos = new Point(0,0);
            for (int i = 0; i < cons.Length; i++)
            {
                // Add character
                characters.Add(cons[i], new Rectangle(charPos.X, charPos.Y, size.X, size.Y));

                // Fix position
                charPos.X += size.X;
                if (i == 6) charPos = new Point(0, size.Y);
            }
            // Sets vowels
            charPos = new Point(0, size.Y * 2);
            for (int i = 0; i < vowels.Length; i++)
            {
                // Add character
                characters.Add(vowels[i], new Rectangle(charPos.X, charPos.Y, size.X, size.Y));

                // Fix position
                charPos.X += size.X;
            }
        }

        // Loads texture and memes alot
        public void LoadFont(Texture2D fontTexture)
        {
            // Loads from path and memes a bit
            this.fontTexture = fontTexture;
            size.X = fontTexture.Width / 8;
            size.Y = fontTexture.Height / 3;

            // sets default spacing
            LineSpacing = size.Y + 1;
            CharSpacing = size.X + 2;

            // Creates dictionary
            characters = new Dictionary<char, Rectangle>();

            // Sets consonants
            Point charPos = new Point(0, 0);
            for (int i = 0; i < cons.Length; i++)
            {
                // Add character
                characters.Add(cons[i], new Rectangle(charPos.X, charPos.Y, size.X, size.Y));

                // Fix position
                charPos.X += size.X;
                if (i == 6) charPos = new Point(0, size.Y);
            }
            // Sets vowels
            charPos = new Point(0, size.Y * 2);
            for (int i = 0; i < vowels.Length; i++)
            {
                // Add character
                characters.Add(vowels[i], new Rectangle(charPos.X, charPos.Y, size.X, size.Y));

                // Fix position
                charPos.X += size.X;
            }
        }

        // Draws a string
        public void DrawString(SpriteBatch spriteBatch, string s, Vector2 pos, Color color)
        {
            Vector2 rp = new Vector2(0, 0);
            for (int i = 0; i < s.Length; i++)
            {
                // Check what type of thing it is
                if (isCons(s[i]))
                {
                    // draws consonant
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[i]], layerDepth: 0, color: color);

                    // possibly draw a vowel
                    if (i != s.Length - 1)
                    {
                        if (isVowel(s[i + 1]))
                        {
                            i++;
                            spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[i]], layerDepth: 0, color: color);
                        }
                    }

                    // meme relative position
                    rp.X += CharSpacing;

                } else if (isVowel(s[i]))
                {
                    // Draw the H before the vowel
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters['H'], layerDepth: 0, color: color);
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[i]], layerDepth: 0, color: color);

                    // Meme relative position
                    rp.X += CharSpacing;

                } else if (s[i] == ' ')
                {
                    // only meme relative position
                    rp.X += 6;

                } else if (s[i] == '\n')
                {
                    // only meme position here as well
                    rp.X = 0;
                    rp.Y += LineSpacing;
                }
            }
        }

        // Measures a string and returns vector of width and height
        public Vector2 MeasureString(string s)
        {
            // VEctor that will be memed
            Vector2 bounds = new Vector2(0, 0);

            string[] lines = s.Split('\n');

            // find X size
            for (int i = 0; i < lines.Length; i++)
            {
                float tempSize = 0;
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (isCons(lines[i][j]) )
                    {
                        tempSize += CharSpacing;
                        if (j != lines[i].Length - 1)
                            if (isVowel(lines[i][j + 1])) j++;
                    } else if (isVowel(lines[i][j]))
                    {
                        tempSize += CharSpacing;
                    }else if (lines[i][j] == ' ')
                    {
                        tempSize += 6;
                    }
                }
                if (tempSize > bounds.X) bounds.X = tempSize;
            }

            // get y size
            bounds.Y = lines.Length * LineSpacing + size.Y;

            return bounds;
        }

        // Makes normal string gahameified
        public static string Gahamefy(string s)
        {
            string newString;
            newString = s.ToUpper();
            newString = newString.Replace('T', 'D');
            newString = newString.Replace('G', 'K');
            newString = newString.Replace('V', 'W');

            return newString;
        }

        // Makes string out of char
        string cts(char c)
        {
            return new string(c, 1);
        }

        // Check if a character is a consonant
        bool isCons(char c)
        {
            for (int i = 0; i < cons.Length; i++)
            {
                if (c == cons[i]) return true;
            }
            return false;
        }

        // Check if a character is a vowel
        bool isVowel(char c)
        {
            for (int i = 0; i < vowels.Length; i++)
            {
                if (c == vowels[i]) return true;
            }
            return false;
        }
    }
}

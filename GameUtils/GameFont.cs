using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;
using System;

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

        // Space sicerino
        public int SpaceSize = 4;

        // Point for width and height
        public Point Size;

        // Loads texture and memes alot
        public void LoadFont(ContentManager content, string path)
        {
            // Loads from path and memes a bit
            fontTexture = content.Load<Texture2D>(path);
            Size.X = fontTexture.Width / 8;
            Size.Y = fontTexture.Height / 3;

            // sets default spacing
            LineSpacing = Size.Y + 1;
            CharSpacing = Size.X + 2;

            // Creates dictionary
            characters = new Dictionary<char, Rectangle>();

            // Sets consonants
            Point charPos = new Point(0,0);
            for (int i = 0; i < TextRenderer.cons.Length; i++)
            {
                // Add character
                characters.Add(TextRenderer.cons[i], new Rectangle(charPos.X, charPos.Y, Size.X, Size.Y));

                // Fix position
                charPos.X += Size.X;
                if (i == 6) charPos = new Point(0, Size.Y);
            }
            // Sets vowels
            charPos = new Point(0, Size.Y * 2);
            for (int i = 0; i < TextRenderer.vowels.Length; i++)
            {
                // Add character
                characters.Add(TextRenderer.vowels[i], new Rectangle(charPos.X, charPos.Y, Size.X, Size.Y));

                // Fix position
                charPos.X += Size.X;
            }
        }

        // Loads texture and memes alot
        public void LoadFont(Texture2D fontTexture)
        {
            // Loads from path and memes a bit
            this.fontTexture = fontTexture;
            Size.X = fontTexture.Width / 8;
            Size.Y = fontTexture.Height / 3;

            // sets default spacing
            LineSpacing = Size.Y + 1;
            CharSpacing = Size.X + 2;

            // Creates dictionary
            characters = new Dictionary<char, Rectangle>();

            // Sets consonants
            Point charPos = new Point(0, 0);
            for (int i = 0; i < TextRenderer.cons.Length; i++)
            {
                // Add character
                characters.Add(TextRenderer.cons[i], new Rectangle(charPos.X, charPos.Y, Size.X, Size.Y));

                // Fix position
                charPos.X += Size.X;
                if (i == 6) charPos = new Point(0, Size.Y);
            }
            // Sets vowels
            charPos = new Point(0, Size.Y * 2);
            for (int i = 0; i < TextRenderer.vowels.Length; i++)
            {
                // Add character
                characters.Add(TextRenderer.vowels[i], new Rectangle(charPos.X, charPos.Y, Size.X, Size.Y));

                // Fix position
                charPos.X += Size.X;
            }
        }

        // Draws a string
        public void DrawString(SpriteBatch spriteBatch, string s, Vector2 pos, Color color)
        {
            Vector2 rp = new Vector2(0, 0);
            for (int i = 0; i < s.Length; i++)
            {
                // Check what type of thing it is
                if (TextRenderer.IsCons(s[i]))
                {
                    // draws consonant
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[i]], layerDepth: 0, color: color);

                    // possibly draw a vowel
                    if (i != s.Length - 1)
                    {
                        if (TextRenderer.IsVowel(s[i + 1]))
                        {
                            spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[++i]], layerDepth: 0, color: color);
                        }
                    }

                    // meme relative position
                    rp.X += CharSpacing;

                } else if (TextRenderer.IsVowel(s[i]))
                {
                    // Draw the H before the vowel
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters['H'], layerDepth: 0, color: color);
                    spriteBatch.Draw(fontTexture, pos + rp, sourceRectangle: characters[s[i]], layerDepth: 0, color: color);

                    // Meme relative position
                    rp.X += CharSpacing;

                } else if (s[i] == ' ')
                {
                    // only meme relative position
                    rp.X += SpaceSize;

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

            // Gahamefy that string
            s = TextRenderer.Gahamefy(s);

            string[] lines = s.Split('\n');

            // find X size
            for (int i = 0; i < lines.Length; i++)
            {
                float tempSize = 0;
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (TextRenderer.IsCons(lines[i][j]) )
                    {
                        tempSize += CharSpacing;
                        if (j != lines[i].Length - 1)
                            if (TextRenderer.IsVowel(lines[i][j + 1])) j++;
                    } else if (TextRenderer.IsVowel(lines[i][j]))
                    {
                        tempSize += CharSpacing;
                    }else if (lines[i][j] == ' ')
                    {
                        tempSize += SpaceSize;
                    }
                }
                if (tempSize > bounds.X) bounds.X = tempSize;
            }

            // get y size
            bounds.Y = lines.Length * LineSpacing + Size.Y;

            return bounds;
        }
    }
}

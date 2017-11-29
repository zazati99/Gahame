﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Gahame.GameUtils;

namespace Gahame.GameObjects.ObjectComponents.Dialogue
{
    public class DialogueBox
    {
        // Text in tha box
        public string Text;

        // Font that will be used to draw tha text
        public SpriteFont Font;

        // Position of the Box
        public Vector2 Position;

        // Char index
        public float CharIndex;

        // Update speed
        public float UpdateSpeed;

        // Skipable?
        public bool Skippable;

        // Constructor with default settings
        public DialogueBox()
        {
            // Le importante  variables
            CharIndex = 0;
            UpdateSpeed = .25f;
            Skippable = true;

            // Default font
            Font = GameFonts.Arial;
        }

        // Draw tha box
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Updates text
            if (CharIndex < Text.Length)
            {
                CharIndex += UpdateSpeed;
            }

            spriteBatch.DrawString(Font,
                (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                Position,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0);
        }

    }
}

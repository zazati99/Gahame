using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Gahame.GameUtils;
using Gahame.GameScreens;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class DialogueBox
    {
        // Text in tha box
        public string Text;

        // Font that will be used to draw tha text
        public SpriteFont Font;

        // Char index
        public float CharIndex;

        // Update speed
        public float UpdateSpeed;

        // Skipable?
        public bool Skippable;

        // Font size
        public int FontSize;

        // The group that holds this box
        protected DialogueBoxGroup group;

        // Constructor with default settings
        public DialogueBox(DialogueBoxGroup group)
        {
            // Le importante  variables
            CharIndex = 0;
            UpdateSpeed = .25f;
            Skippable = true;
            this.group = group;

            // Default font
            Font = GameFonts.Arial;

            // Default fontsize
            FontSize = 8;
        }

        // Update the box
        public virtual void Update(GameTime gameTime)
        {
            
        }

        // Draw tha box
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        // Add text
        public void AddText(string Text)
        {
            this.Text = Text;
        }

    }
}

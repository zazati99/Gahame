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

        // Position of the Box
        public Vector2 Position;
        Vector2 origin;
        Vector2 size;

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

            // Position stuff
            origin = new Vector2();
            size = new Vector2(201 , 50);
            Position = new Vector2(CameraController.ViewWidth()/2 - 100, CameraController.ViewHeight() - 60);

            Font.LineSpacing = 17;
            Font.LineSpacing = (int)(size.Y / 2)-8;
        }

        // Draw tha box
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Updates text
            if (CharIndex < Text.Length)
            {
                CharIndex += UpdateSpeed;
            }

            // Draws the box
            ShapeRenderer.FillRectangle(
                spriteBatch,
                CameraController.PositionOnScreen(Position + new Vector2(-1, 0)),
                size,
                0.01f,
                Color.Black);

            // Draws the text
            spriteBatch.DrawString(
                Font,
                (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                CameraController.PositionOnScreen(Position),
                Color.White,
                0,
                origin,
                1,
                SpriteEffects.None,
                0);
        }

        // Add text
        public void AddText(string Text)
        {
            this.Text = Text;
        }

    }
}

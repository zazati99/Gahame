using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class DialogueBoxPlain : DialogueBox
    {

        // Position of the Box
        public Vector2 Position;
        Vector2 origin;
        Vector2 size;

        // COnstructor for box
        public DialogueBoxPlain(DialogueBranch group) : base(group)
        {
            // Position stuff
            origin = new Vector2();
            size = new Vector2(202, 50);
            Position = new Vector2(Camera.View.X / 2 - 100, Camera.View.Y - 60);

            Font.LineSpacing = (int)(size.Y / 2) - FontSize;
            FontSize = 8;
            //GameFonts.GahameFont.LineSpacing = (int)(size.Y / 2) - FontSize;
        }

        // Updates Box
        public override void Update(GameTime gameTime)
        {
            // Updates text
            if (CharIndex < Text.Length)
            {
                CharIndex += UpdateSpeed;
            }

            // Change box or stop dialogue
            if (GameInput.Activate && !GameInput.ActivateCD)
            {
                // Checks if all text is there
                if (CharIndex >= Text.Length)
                {
                    // stop dialogue
                    if (branch.CurrentBox == branch.Boxes.Count - 1)
                    {
                        branch.dialogue.StopDialogue();
                    }
                    // Or go to next box
                    else
                    {
                        branch.CurrentBox++;
                    }
                }
                // Fixes all of the text meme
                else if (Skippable) CharIndex = Text.Length;
            }
        }

        // Draws box
        public override void Draw(SpriteBatch spriteBatch)
        {
            Position.Y = Camera.View.Y - 60;

            // Draws the box
            ShapeRenderer.FillRectangle(
                spriteBatch,
                Position + new Vector2(-1, 0),
                size,
                0.01f,
                Color.Black);

            // Draws the text
            spriteBatch.DrawString(
                Font,
                (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                Position,
                Color.White,
                0,
                origin,
                1,
                SpriteEffects.None,
                0);

            //GameFonts.GahameFont.DrawString(spriteBatch, GameFont.Gahamefy((CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text), Position+new Vector2(1, 4), Color.White);

        }
    }
}

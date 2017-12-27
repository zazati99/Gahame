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
        public DialogueBoxPlain(DialogueBoxGroup group) : base(group)
        {
            // Position stuff
            origin = new Vector2();
            size = new Vector2(202, 50);
            Position = new Vector2(Camera.View.X - 100, Camera.View.Y - 60);

            Font.LineSpacing = (int)(size.Y / 2) - FontSize;
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
            if (GameControlls.Activate && !GameControlls.ActivateCD)
            {
                // Checks if all text is there
                if (CharIndex >= Text.Length)
                {
                    // stop dialogue
                    if (group.CurrentBox == group.Boxes.Count - 1)
                    {
                        group.ClearGroup();
                        group.dialogue.StopDialogue();
                    }
                    // Or go to next box
                    else
                    {
                        group.CurrentBox++;
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
                Camera.PositionOnScreen(Position + new Vector2(-1, 0)),
                size,
                0.01f,
                Color.Black);

            // Draws the text
            spriteBatch.DrawString(
                Font,
                (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                Camera.PositionOnScreen(Position),
                Color.White,
                0,
                origin,
                1,
                SpriteEffects.None,
                0);
        }

    }
}

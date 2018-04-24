using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class DialogueBoxPlain : DialogueBox
    {

        // How draw it??!?!?
        public TextType textType;
        public enum TextType
        {
            NORMAL, SHAKING, WAVING
        };

        // Position of the Box
        public Vector2 Position;
        Vector2 offset;
        Vector2 size;

        // COnstructor for box
        public DialogueBoxPlain(DialogueBranch group) : base(group)
        {
            // Position stuff
            offset = new Vector2(1, 2);
            size = new Vector2(202, 50);
            Position = new Vector2(Camera.View.X / 2 - 100, Camera.View.Y - 60);

            Font.LineSpacing = (int)(size.Y / 2) - FontSize;
            FontSize = 10;
            GameFonts.GahameFont.LineSpacing = (int)(size.Y / 2) - FontSize;

            textType = TextType.NORMAL;
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
            ShapeRenderer.FillRectangle
            (
                spriteBatch,
                Position + new Vector2(-1, 0),
                size,
                0.01f,
                Color.Black
            );

            switch (textType)
            {
                case TextType.NORMAL:

                    TextRenderer.DrawMixedText
                    (
                        spriteBatch,
                        Font,
                        GameFonts.GahameFont,
                        (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                        Position + offset,
                        Color.White
                    );

                    break;
                case TextType.SHAKING:

                    TextRenderer.DrawMixedShakingText
                    (
                        spriteBatch,
                        Font,
                        GameFonts.GahameFont,
                        (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                        Position + offset,
                        .25f,
                        Color.White
                    );

                    break;
                case TextType.WAVING:

                    TextRenderer.DrawMixedWaveText
                    (
                        spriteBatch,
                        Font,
                        GameFonts.GahameFont,
                        (CharIndex < Text.Length) ? Text.Remove((int)CharIndex) : Text,
                        Position + offset,
                        .35f,
                        Color.White
                    );

                    break;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class DialogueBranch
    {
        // Dialogue that holds this gropus
        public Dialogue dialogue;

        // Dialogue boxes
        public List<DialogueBox> Boxes;

        // Current box in group
        public int CurrentBox;

        // Constructor
        public DialogueBranch(Dialogue dialogue)
        {
            // variable stuff
            this.dialogue = dialogue;
            Boxes = new List<DialogueBox>();
            CurrentBox = 0;
        }

        // Updates the boxes
        public void Update(GameTime gameTime)
        {
            Boxes[CurrentBox].Update(gameTime);
        }

        // Draws the boxes
        public void Draw(SpriteBatch spriteBatch)
        {
            Boxes[CurrentBox].Draw(spriteBatch);
        }

        // Clears the group before it moves on
        public void ClearGroup()
        {
            // Resets all of the boxes
            for(int i = 0; i < Boxes.Count; i++)
            {
                Boxes[i].CharIndex = 0;
            }
            // Resets box
            CurrentBox = 0;
        }

    }
}

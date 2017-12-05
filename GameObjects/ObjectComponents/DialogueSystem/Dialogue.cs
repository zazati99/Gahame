using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameObjects.ObjectComponents.DialogueSystem
{
    public class Dialogue : ObjectComponent
    {
        // My boxes
        public List<DialogueBox> Boxes;
        public int CurrentBox;

        // Accesible?
        public bool Accesible;

        // Dialogue constructor
        public Dialogue(GameObject o) : base(o)
        {
            // Box variables
            Boxes = new List<DialogueBox>();
            CurrentBox = 0;

            // Important component variables
            Drawable = false; // becomes true when active
            Updatable = false;

            // Accesible Meme 
            Accesible = true;
        }

        // Draw all of the stuffs
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Change box or stop dialogue
            if (GameControlls.E && !Accesible)
            {
                // Checks if all text is there
                if ((int)Boxes[CurrentBox].CharIndex >= Boxes[CurrentBox].Text.Length)
                {
                    // stop dialogue
                    if (CurrentBox == Boxes.Count-1)
                    {
                        StopDialogue();
                    }
                    // Or go to next box
                    else CurrentBox++;
                }
                // Fixes all of the text meme
                else if(Boxes[CurrentBox].Skippable) Boxes[CurrentBox].CharIndex = Boxes[CurrentBox].Text.Length;
            }
            else Accesible = false;

            // Does the rest of the drawing if it's drawable
            if (Drawable)
            {
                Boxes[CurrentBox].Draw(spriteBatch);
            }
        }

        // Starts the dialoguie
        public void StartDialogue()
        {
            Drawable = true;
        }

        // Stops the dialogue
        public void StopDialogue()
        {
            Accesible = true;
            Drawable = false;
            CurrentBox = 0;

            // Resets Boxes
            for (int i = 0; i < Boxes.Count; i++)
            {
                Boxes[i].CharIndex = 0;
            }
        }

    }
}

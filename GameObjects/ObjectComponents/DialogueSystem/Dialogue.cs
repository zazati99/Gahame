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
        public Dictionary<string, DialogueBoxGroup> BoxGroups;

        // Key to the box
        public string Key;

        // Accesible?
        public bool Accesible;

        // Dialogue constructor
        public Dialogue(GameObject o) : base(o)
        {
            // Box variables
            BoxGroups = new Dictionary<string, DialogueBoxGroup>();

            // Important component variables
            Drawable = false; // becomes true when active
            Updatable = false; // becomes true when active

            // Key starts with 0
            Key = "";

            // Accesible Meme 
            Accesible = true;
        }

        public override void Update(GameTime gameTime)
        {
            BoxGroups[Key].Update(gameTime);
        }

        // Draw all of the stuffs
        public override void Draw(SpriteBatch spriteBatch)
        {
            BoxGroups[Key].Draw(spriteBatch);
        }

        // Starts the dialoguie
        public void StartDialogue()
        {
            Drawable = true;
            Updatable = true;

            GahameController.CutScene = true;
        }

        // Stops the dialogue
        public void StopDialogue()
        {
            Drawable = false;
            Updatable = false;

            GahameController.CutScene = false;
            Key = "";
        }

    }
}

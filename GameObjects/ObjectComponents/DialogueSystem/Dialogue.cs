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
        // My boxes (needs secret key to be unlocked)
        public Dictionary<string, DialogueBranch> DialogueBranches;

        // Key to the box
        public string Key;

        // Accesible?
        public bool Accesible;

        // Dialogue constructor
        public Dialogue(GameObject o) : base(o)
        {
            // Box variables
            DialogueBranches = new Dictionary<string, DialogueBranch>();

            // Important component variables
            Drawable = false; // becomes true when active
            Updatable = false; // becomes true when active

            // Key starts with ""
            Key = "";

            // Accesible Meme 
            Accesible = true;
        }

        public override void Update(GameTime gameTime)
        {
            DialogueBranches[Key].Update(gameTime);
        }

        // Draw all of the stuffs
        public override void Draw(SpriteBatch spriteBatch)
        {
            DialogueBranches[Key].Draw(spriteBatch);
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
            // Loop through branches to stop this madness
            foreach (KeyValuePair<string, DialogueBranch> entry in DialogueBranches)
            {
                // clears group
                entry.Value.ClearGroup();
            }

            // sets drawable and updatable to false
            Drawable = false;
            Updatable = false;

            // go back to default stuff
            GahameController.CutScene = false;
            Key = "";
        }

    }
}

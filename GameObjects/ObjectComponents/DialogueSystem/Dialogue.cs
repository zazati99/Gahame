﻿using System;
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

        // Key to the branch
        public string Key;

        // Accesible?
        public bool Accesible;

        // is talking?
        public bool IsTalking;

        // Dialogue constructor
        public Dialogue(GameObject o) : base(o)
        {
            // Box variables
            DialogueBranches = new Dictionary<string, DialogueBranch>();

            IsTalking = false;

            // Key starts with "" by default
            Key = "";

            // Accesible Meme 
            Accesible = true;
        }

        // Updates text
        public override void Update(GameTime gameTime)
        {
            if (IsTalking) DialogueBranches[Key].Update(gameTime);
        }

        // Draw all of the stuffs
        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            if (IsTalking) DialogueBranches[Key].Draw(spriteBatch);
        }

        // Starts the dialoguie
        public void StartDialogue()
        {
            IsTalking = true;
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

            IsTalking = false;

            // go back to default stuff
            GahameController.CutScene = false;
            Key = "";
        }

    }
}

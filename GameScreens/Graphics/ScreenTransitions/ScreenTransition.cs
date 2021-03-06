﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class ScreenTransition
    {
        // The screen that will be cnahged to
        protected GameScreen newScreen;

        // The Current screen, might be used to do screeneffects and camera stuff
        protected GameScreen currentScreen;

        // Should it clear old screen??!?!
        bool clearOldScreen;

        // Constructor
        public ScreenTransition(GameScreen currentScreen, GameScreen screen, bool clearOldScreen)
        {
            // Sets new screen
            newScreen = screen;

            // Gets current screen
            this.currentScreen = currentScreen;

            // Clererino of old screen?
            this.clearOldScreen = clearOldScreen;
        }

        // Start transition
        public virtual void StartTransition()
        {
            // It's a cutscene baby
            GahameController.CutScene = true;
        }

        // Stop transition
        public virtual void StopTransition()
        {
            //It's not a cuscene baby :(
            GahameController.CutScene = false;
            ScreenManager.Instance.ScreenTransition = false;
            ScreenManager.Instance.Transition = null;
        }

        // Update screen transition
        public virtual void Update(GameTime gameTime)
        {

        } 

        // Draw Screen transtiion
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        // Draws possible gui thing on screenTransition
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }

        // Change to the new Screen
        protected virtual void ChangeScreen()
        {
            // Change and clear old screen
            if (clearOldScreen)
            {
                ScreenManager.Instance.ChangeScreenClear(newScreen);
            }
            // CHange but do not clear old screen
            else
            {
                ScreenManager.Instance.ChangeScreen(newScreen);
            }
        }
    }
}

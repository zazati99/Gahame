using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameScreens
{
    public class ScreenTransition
    {
        // The screen that will be cnahged to
        GameScreen newScreen;

        // The Current screen, might be used to do screeneffects and camera stuff
        GameScreen currentScreen;

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

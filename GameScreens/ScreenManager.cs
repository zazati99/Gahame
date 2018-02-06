using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class ScreenManager
    {
        // The Game camera
        public Camera GameCamera;
 
        // The current GameScreen 
        GameScreen currentScreen;

        // The content manager boy
        public ContentManager Content;

        // the screenyboy
        static ScreenManager instance;

        // Screen transition (will prob be null a lot)
        public ScreenTransition Transition;

        // are we transitioning screens
        public bool ScreenTransition;

        // Next screen (can be loaded on separate thread in background)
        public GameScreen NextScreen;
        bool nextScreenReady;

        // Public thing så att man kan komma åt 'at överallt
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        // Initialize I guess?
        public void Initialize()
        {
            // screentransition is false by default
            ScreenTransition = false;
        }

        // Load content boy
        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
#if DEBUG
            currentScreen = GameFileMaganer.LoadScreenFromPath("Content/DEBUG_LEVEL.sml");
            //currentScreen = GameFileMaganer.LoadScreenFromEmbeddedPath("TestLevel.sml");
#else
            currentScreen = GameFileMaganer.LoadScreenFromEmbeddedPath("TestLevel.sml");
#endif
            // Load content below here

        }

        // Unload those mean bois
        public void UnloadContent()
        {
            Content.Unload();
        }
        
        // Update all of the logicz here
        public void Update(GameTime gameTime)
        {
            //if (GameInput.F6) GoToNextScreen(new ScreenTransitionFade(.075f ,currentScreen, nextScreen, true));
            if (GameInput.F6) GoToNextScreen(new ScreenTransitionRectangle(currentScreen, NextScreen, true));
            if (GameInput.F5) GameCamera.SwitchFullscreen();
            if (GameInput.Enter)
            {
                Random r = new Random();
                GahameController.Seed = r.Next();
                ChangeScreenClear(GameFileMaganer.LoadScreenFromEmbeddedPath("TestLevel.sml"));
            }

            // Updates the current Screen
            currentScreen.Update(gameTime);

            // Update screen transition
            if (ScreenTransition)
            {
                Transition.Update(gameTime);
            }
        }

        // Draw all of the master artwork right here
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws everything in ccurrent screen
            currentScreen.Draw(spriteBatch);

            // Draws screen transition
            if (ScreenTransition)
            {
                Transition.Draw(spriteBatch);
            }
        }

        // Draws gui on another SpriteBatch
        public void DrawGUI(SpriteBatch spriteBatch)
        {
            // Draws GUI from current screen
            currentScreen.DrawGUI(spriteBatch);

            // Draws screen transition
            if (ScreenTransition)
            {
                Transition.DrawGUI(spriteBatch);
            }
        }

        // GameScreen Functions

        // DIFERENT WAYS OF LOADING SCREEN BELOW

        // Change the current gamescreen and clears the old one but does not load content
        public void ChangeScreenClear(GameScreen screen)
        {
            currentScreen.UnloadContent();
            currentScreen = null;

            GC.Collect();

            currentScreen = screen;
            currentScreen.Start();
        }

        // Change Screen without loading content
        public void ChangeScreen(GameScreen screen)
        {
            currentScreen = screen;
            screen.Start();
        }

        // Change screen with a transition
        public void ChangeScreen(ScreenTransition transition)
        {   
            if (!ScreenTransition)
            {
                ScreenTransition = true;
                Transition = transition;
                Transition.StartTransition();
            }
        }

        // Load next screen 
        public void LoadNextScreen(string path)
        {
            nextScreenReady = false;
            new Thread(() =>
            {
                NextScreen = GameFileMaganer.LoadScreenFromEmbeddedPath(path);
                nextScreenReady = true;
            }).Start();
        }
        // Go to next screen
        public void GoToNextScreen()
        {
            // Go to screen if it's ready
            if (nextScreenReady)
            {
                ChangeScreenClear(NextScreen);
                nextScreenReady = false;
                NextScreen = null;
            }
        }
        // Go to next screen BUT WITH A COOL TRANSITION
        public void GoToNextScreen(ScreenTransition transition)
        {
            // Go to screen if it's ready
            if (nextScreenReady)
            {
                ChangeScreen(transition);
                nextScreenReady = false;
                NextScreen = null;
            }
        }

    }
}

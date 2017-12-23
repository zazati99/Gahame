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

        // SCREEN VARIABLER OCH SÅNT BÖRJAR HÄR
        public GraphicsDeviceManager Graphics;
        public static int DefaultViewportX;
        public static int DefaultViewportY;
        public static int ViewportX;
        public static int ViewportY;

        public void DefaultViewPort()
        {
            ViewportX = DefaultViewportX;
            ViewportY = DefaultViewportY;
            Graphics.PreferredBackBufferWidth = ViewportX;
            Graphics.PreferredBackBufferHeight = ViewportY;

            Graphics.ApplyChanges();
        }
        public void ChangeViewPort(int x, int y)
        {
            ViewportX = x;
            ViewportY = y;
            Graphics.PreferredBackBufferWidth = ViewportX;
            Graphics.PreferredBackBufferHeight = ViewportY;

            Graphics.ApplyChanges();
        }
        public void SwitchFullscreen()
        {
            Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();
            if (Graphics.IsFullScreen)
            {
                int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                ChangeViewPort(screenWidth, screenHeight);
                float newZoom = screenWidth / (DefaultViewportX / 2f);
                Game1.cam.Zoom = newZoom;
            }
            else
            {
                DefaultViewPort();
                Game1.cam.Zoom = 2;
            }
        }
        // HÄR SLUTAR SCREEN VARIABLE KAOSET




        // The current GameScreen 
        GameScreen currentScreen;

        // The content manager boy
        public ContentManager Content;

        // the screenyboy
        static ScreenManager instance;

        // Next screen (can be loaded on separate thread in background)
        public GameScreen nextScreen;
        bool nextScreenReady;
        string nextScreenPath;

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
            
        }

        // Load content boy
        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
#if DEBUG
            currentScreen = GameFileMaganer.LoadScreenEmbedded("TestLevel.sml");
#else
            currentScreen = GameFileMaganer.LoadScreenEmbedded("TestLevel.sml");
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
            if (GameControlls.F6) GoToNextScreen();
            if (GameControlls.F5) SwitchFullscreen();
            if (GameControlls.Enter)
            {
                Random r = new Random();
                GahameController.Seed = r.Next();
                ChangeScreenClear(GameFileMaganer.LoadScreenEmbedded("TestLevel.sml"));
            }
            // Updates the current Screen
            currentScreen.Update(gameTime);
        }

        // Draw all of the master artwork right here
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws everything in ccurrent screen
            currentScreen.Draw(spriteBatch);
        }

        // GameScreen Functions

        // DIFERENT WAYS OF LOADING SCREEN BELOW
        // Change the current gamescreen and clears the old one
        public void ChangeScreenClearLoad(GameScreen screen)
        {
            currentScreen.UnloadContent();
            currentScreen = null;

            GC.Collect();

            currentScreen = screen;
            currentScreen.LoadContent();
            currentScreen.Start();
        }
        // Change the current gamescreen and clears the old one but does not load content
        public void ChangeScreenClear(GameScreen screen)
        {
            currentScreen.UnloadContent();
            currentScreen = null;

            GC.Collect();

            currentScreen = screen;
            currentScreen.Start();
        }
        // Change the current game screen without deleting old one
        public void ChangeScreenLoad(GameScreen screen)
        {
            currentScreen = screen;
            currentScreen.LoadContent();
            currentScreen.Start();
        }
        // Change Screen without loading content
        public void ChangeScreen(GameScreen screen)
        {
            currentScreen = screen;
            screen.Start();
        }

        // Load next screen
        public void LoadNextScreen(string path)
        {
            nextScreenReady = false;
            new Thread(() =>
            {
                nextScreen = GameFileMaganer.LoadScreenEmbedded(path);
                nextScreenReady = true;
            }).Start();
        }
        // Go to next screen
        public void GoToNextScreen()
        {
            // Go to screen if it's ready
            if (nextScreenReady)
            {
                ChangeScreenClear(nextScreen);
                nextScreenReady = false;
                nextScreen = null;
            }
        }

    }
}

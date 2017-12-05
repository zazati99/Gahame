using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void switchFullscreen()
        {
            Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();
            if (Graphics.IsFullScreen)
            {
                int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                ChangeViewPort(screenWidth, screenWidth * 9/16);
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
            currentScreen = GameFileMaganer.LoadScreen("Content/Debug.level");
#else
            //currentScreen = GameFileMaganer.LoadScreen("Content/TestLevel.sml");
            currentScreen = GameFileMaganer.LoadScreenEmbedded("Gahame.TestLevel.sml");
#endif
            //GameFileMaganer.EncryptFile("Content/Encrypted.sml", "Content/testeroni.sml");
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
            if (GameControlls.F5) switchFullscreen();
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

        // ChangeScreen
        public void ChangeScreen(GameScreen screen)
        {
            currentScreen.UnloadContent();
            currentScreen = null;

            GC.Collect();

            currentScreen = screen;
            currentScreen.LoadContent();
        }

    }
}

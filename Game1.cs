using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameScreens;
using Gahame.GameUtils;

using System;

namespace Gahame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameFont fnt;

        public Game1()
        {
            // Content settings
            Content.RootDirectory = "Content";

            // Some graphics settings
            graphics = new GraphicsDeviceManager(this);
            graphics.HardwareModeSwitch = false;
            graphics.GraphicsProfile = GraphicsProfile.Reach;

            // Randomizes seed
            Random r = new Random();
            GahameController.Seed = r.Next();

            // Camera
            Vector2 defaultView = new Vector2(320, 180); // the game view
            Vector2 defaultPort = new Vector2(640, 360); // the window size
            ScreenManager.Instance.GameCamera = new Camera(defaultPort, defaultView, graphics);

            //IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
            //TimeSpan span = new TimeSpan(0, 0, 0, 0, 1);
            //TargetElapsedTime = span;

            // TEST GARBAGE
            fnt = new GameFont();
        }

        protected override void Initialize()
        {
            // Initialize stuff here I guess?
            ScreenManager.Instance.Initialize();
            base.Initialize();

            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load spriteFonts here
            GameFonts.LoadFonts(Content);

            // Load all fo that sweet sweet content right here
            ScreenManager.Instance.LoadContent(Content);

            // TEST GARBAGE
            fnt.LoadFont(Content, "Fonts/GahameFont");
        }

        protected override void UnloadContent()
        {
            // Unload that stinky old content here
            ScreenManager.Instance.UnloadContent();
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Updates controlls before updating anythin else
            GameInput.Update();

            // Update stuff here
            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw stuff between these bad boys
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                ScreenManager.Instance.GameCamera.GetTransformation());

            // Call Draw in screenManager (basically draws all of the game)
            ScreenManager.Instance.Draw(spriteBatch);

            // End spriteBatch, can't draw beyond this
            spriteBatch.End();

            // SpriteBatch for ui
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                ScreenManager.Instance.GameCamera.UiTransformation());

            // Draws gui in screens
            ScreenManager.Instance.DrawGUI(spriteBatch);

            // Draw fps counter (test stuff)
            float fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            string fpsString = string.Format("{0:N3}", fps);
            spriteBatch.DrawString(GameFonts.Arial, fpsString, new Vector2(15, 10), Color.Black, 0, Vector2.One, 1, SpriteEffects.None, 0);

            // Draws the current gamespeed (test stuff)
            string gameSpeed = string.Format("{0:N3}", GahameController.GameSpeed);
            spriteBatch.DrawString(GameFonts.Arial, gameSpeed, new Vector2(15, 21), Color.Black, 0, Vector2.One, 1, SpriteEffects.None, 0);

            // Shows if game is in controller mode or not (test stuff)
            spriteBatch.DrawString(GameFonts.Arial, GameInput.ControllerMode ? "ControllerMode" : "KeyboardMode", new Vector2(15, 31), Color.Black, 0, Vector2.One, 1, SpriteEffects.None, 0);

            fnt.DrawString(spriteBatch, GameFont.Gahamefy("gahame"), new Vector2(15, 45));

            // End this SpriteBatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

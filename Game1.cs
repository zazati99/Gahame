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

        public static GameCamera cam;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenManager.Instance.Graphics = graphics;

            graphics.HardwareModeSwitch = false;
            graphics.GraphicsProfile = GraphicsProfile.Reach;

            ScreenManager.DefaultViewportX = 640;
            ScreenManager.DefaultViewportY = 360;
            ScreenManager.Instance.DefaultViewPort();

            Random r = new Random();
            GahameController.Seed = r.Next();

            //IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;

            cam = new GameCamera
            {
                 Pos = Vector2.Zero,
                 Zoom = 2
            };
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
            GameControlls.Update();

            // Update stuff here
            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw stuff between these bad boys
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null,null,null, cam.get_transformation(graphics.GraphicsDevice));

            ScreenManager.Instance.Draw(spriteBatch);

            float fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            string fpsString = string.Format("{0:N3}", fps);
            spriteBatch.DrawString(GameFonts.Arial, fpsString , GameObjects.CameraController.PositionOnScreen(new Vector2(15, 10)), Color.Black, 0, Vector2.One, 1, SpriteEffects.None, 0);

            string gameSpeed = string.Format("{0:N3}", GahameController.GameSpeed);
            spriteBatch.DrawString(GameFonts.Arial, gameSpeed, GameObjects.CameraController.PositionOnScreen(new Vector2(15, 21)), Color.Black, 0, Vector2.One, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

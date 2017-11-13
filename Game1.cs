using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Gahame.GameScreens;
using Gahame.GameUtils;

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

            cam = new GameCamera();
            cam.Pos = Vector2.Zero;
            cam.Zoom = 2;
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
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

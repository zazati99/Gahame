using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Gahame.GameObjects;

namespace Gahame.GameScreens
{
    public class GameScreen
    {

        // Game camera controller
        public CameraController CamController;

        // ContentManager used by them GameScreens
        public ContentManager content { get; protected set; }

        // List of Gameobjects in the screen
        public List<GameObject> GameObjects;

        // Screen size size
        public Vector2 ScreenSize;

        // Constructor
        public GameScreen()
        {
            ScreenSize = new Vector2(3000, 3000);

            // Cam controller creation
            CamController = new CameraController(null);

            // Creates list
            GameObjects = new List<GameObject>();
        }

        // Getting contentManager from stuff (must haver base thing)
        public virtual void LoadContent()
        {
            // Getting ContentManager from ScreenManager
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        // Unloading content (must also have base thing)
        public virtual void UnloadContent()
        {
            content.Unload();
        }

        // Update thing in GameScreen
        public virtual void Update(GameTime gameTime)
        {
            // Updates all GameObjects on screen
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update(gameTime);
            }
            // Updates gamera
            CamController.Update();
        }

        // Draw things in GameSreen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // calls draw function for all gameObjects on screen
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }
        }

    }
}

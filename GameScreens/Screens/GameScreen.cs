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
        // ContentManager used by them GameScreens
        public ContentManager content { get; protected set; }

        // List of Gameobjects in the screen
        public List<GameObject> GameObjects;

        // List of backgrounds
        public List<ScreenBackground> Backgrounds;

        // List of Screen Effects
        public List<ScreenEffect> ScreenEffects;

        // List of tilesets
        public List<Tileset> Tilesets;

        // Screen size size
        public Vector2 ScreenSize;

        // Default camera position
        public Vector2 DefaultCameraPosition;

        // Camera controller
        public CameraController CamController;

        // Player Object, so that it can be accesed in cutscenes and stuff
        public PlayerObject Player;

        // Name of screen (should be same as path)
        public string Name;

        // Constructor
        public GameScreen()
        {
            // default screen size
            ScreenSize = new Vector2(3000, 3000);

            // Cam controller initialization
            CamController = new CameraController();

            // Creates list of objeects
            GameObjects = new List<GameObject>();

            // Create List of backgrounds
            Backgrounds = new List<ScreenBackground>();

            // Create list of screen effects
            ScreenEffects = new List<ScreenEffect>();

            // Creates List of tilesets
            Tilesets = new List<Tileset>();
        }

        // Gets called when screen is changed to
        public virtual void Start()
        {
            // sets default camera position
            Camera.SetPosition(DefaultCameraPosition);
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
            // Dispose all objects
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].UnloadContent();
            }

            // dispose all backgrounds
            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].UnloadContent();
            }

            // Unload content
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

            // Calls update on screen effects
            for (int i = 0; i < ScreenEffects.Count; i++)
            {
                ScreenEffects[i].Update(gameTime);
            }

            // Updates gamera
            CamController.Update();
        }

        // Draw things in GameSreen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // calls Draw function for backgrounds
            for (int i = 0; i < Backgrounds.Count; i++)
            {
                Backgrounds[i].Draw(spriteBatch);
            }

            // Draws all of the tiles
            for (int i = 0; i < Tilesets.Count; i++)
            {
                Tilesets[i].Draw(spriteBatch);
            }

            // calls draw function for all gameObjects on screen
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }

            // calls draw for each screen effect
            for (int i = 0; i < ScreenEffects.Count; i++)
            {
                ScreenEffects[i].Draw(spriteBatch);
            }
        }

        // Draw GUI
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {
            // Calls the draw for the drawerino of the gui
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].DrawGUI(spriteBatch);
            }
        }

    }
}

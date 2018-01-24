using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameObjects;

using System.Collections.Generic;

namespace Gahame.GameScreens
{
    public class OverworldScreenSidePerspective : GameScreen
    {
        // List of loading areas for other game screens
        List<ScreenLoadArea> loadingAreas;

        // Constructor
        public OverworldScreenSidePerspective() : base()
        {
            // Create List for loading areas
            loadingAreas = new List<ScreenLoadArea>();

            // Load content directly after being called
            LoadContent();
        }

        // Start
        public override void Start()
        {
            base.Start();
        }

        // Loads everything my dude
        public override void LoadContent()
        {
            base.LoadContent();
            // Load stuff and add GameObjects Below 

            ScreenLoadArea a = new ScreenLoadArea();
            a.Position = new Vector2(350, 150);
            a.Size = new Vector2(64, 64);
            a.ScreenName = "TestLevel.sml";
            loadingAreas.Add(a);
        }

        // Unloads all the trash
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        // Updates everything
        public override void Update(GameTime gameTime)
        {
            // Update base
            base.Update(gameTime);

            // Check if player is in loading area
            for (int i = 0; i < loadingAreas.Count; i++)
            {
                loadingAreas[i].CheckCollisionWithPlayer(Player.Position);
            }
        }

        // Draws all of the boys
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Just a little test thing
            for (int i = 0; i < loadingAreas.Count; i++) loadingAreas[i].Draw(spriteBatch);
        }
    }
}

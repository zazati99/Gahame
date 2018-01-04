using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameObjects;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class OverworldScreen : GameScreen 
    {
        // the player
        public PlayerObjectOverworld Player;

        // Has this overworld screen battles?
        public bool HasBattles;

        // List of location of possible battle screens
        public List<string> BattleScreens;

        // List of loading areas for other game screens
        List<ScreenLoadArea> loadingAreas;

        // Random encounter
        Random randomEncounter;

        // Constructor
        public OverworldScreen() : base()
        {
            // BattleSCreens
            BattleScreens = new List<string>();
            BattleScreens.Add("BattleTest.sml");

            // Random encounter
            randomEncounter = new Random();

            // Loading areas
            loadingAreas = new List<ScreenLoadArea>();

            // Test loading area
            ScreenLoadArea a = new ScreenLoadArea();
            a.Size = new Vector2(64, 64);
            a.Position = new Vector2(200, 100);
            a.ScreenName = "TestLevel2.sml";
            loadingAreas.Add(a);

            // HasBattles
            HasBattles = true;

            // Load content directly after being called
            LoadContent();
        }

        // Loads everything my dude
        public override void LoadContent()
        {
            base.LoadContent();
            // Load stuff and add GameObjects Below 

            // Test background
            ScreenBackground bac = new ScreenBackground(this);
            bac.LoadTexture(content, "Backgrounds/testkek");
            bac.RepeatY = true;
            bac.RepeatX = true;
            Backgrounds.Add(bac);

            // Test tiles
            Tileset tset = new Tileset();
            tset.LoadTexture(content, "Backgrounds/tile", new Vector2(4, 4));

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Tile t = new Tile();
                    t.Position = new Vector2(32 + 32 * x, 32 + 32 * y);
                    t.ColumnRow = new Point(x, y);
                    tset.Tiles.Add(t);
                }
            }

            Tilesets.Add(tset);

        }

        // Start initialize some stuff
        public override void Start()
        {
            // Call start from GameScreen
            base.Start();
        }

        // Unloads all the trash
        public override void UnloadContent()
        {
            // Unloads all content
            base.UnloadContent();
        }

        // Updates everything
        public override void Update(GameTime gameTime)
        {
            // Call update in GameScreen
            base.Update(gameTime);

            // Start battle if it has battles
            if (HasBattles) {
                if (Player.GetComponent<Physics>().Velocity != Vector2.Zero)
                {
                    if (randomEncounter.Next(5000) == 1)
                    {
                        GotoBattleScreen();
                    }
                }
            }

            // Check if player is in loading area
            for (int i = 0; i < loadingAreas.Count; i++)
            {
                loadingAreas[i].CheckCollisionWithPlayer(Player.Position);
            }
        }

        // Draws all of the boys
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calls Draw in GameScreen
            base.Draw(spriteBatch);

            // Just a little test thing
            for (int i = 0; i < loadingAreas.Count; i++) loadingAreas[i].Draw(spriteBatch);
        }

        // Go to battle screen
        public void GotoBattleScreen()
        {
            // set camera position
            DefaultCameraPosition = Camera.Position;

            // creates battlescreen
            BattleScreen battleScreen = (BattleScreen)GameFileMaganer.LoadScreenFromEmbeddedPath(BattleScreens[0]);
            battleScreen.PreviousScreen = this;

            // changes the screen
            //ScreenManager.Instance.ChangeScreen(battleScreen);
            ScreenManager.Instance.ChangeScreen(new ScreenTransitionRectangle(this, battleScreen, false));
        }

    }
}

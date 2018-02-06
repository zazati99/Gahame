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
        // Has this overworld screen battles?
        public bool HasBattles;

        // List of location of possible battle screens
        public List<string> BattleScreens;

        // List of loading areas for other game screens
        public List<ScreenLoadArea> LoadingAreas;

        // Random encounter
        Random randomEncounter;

        // TEST CUTSCENE THING
        CutsceneTimer ct;

        // Constructor
        public OverworldScreen() : base()
        {
            // BattleSCreens
            BattleScreens = new List<string>();
            BattleScreens.Add("BattleTest.sml");

            // Random encounter
            randomEncounter = new Random();

            // Loading areas
            LoadingAreas = new List<ScreenLoadArea>();

            // HasBattles
            HasBattles = true;

            // Load content directly after being called
            LoadContent();

            // TEEEST
            ct = new CutsceneTimer();
        }

        // Loads everything my dude
        public override void LoadContent()
        {
            base.LoadContent();
            // Load stuff and add GameObjects Below 
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

            if (GameInput.F6)
            {
                ct.Start();
                GahameController.CutScene = true;
            }
            ct.Update();
            if (ct.EventDuring(60, 120))
            {
                Player.WalkHorizontal(2);
            }
            if (ct.EventDuring(180, 240))
            {
                Player.WalkVertical(2);
            }
            if (ct.EventAtTime(240))
            {
                GahameController.CutScene = false;
                ct.Stop();
            }

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
            for (int i = 0; i < LoadingAreas.Count; i++)
            {
                LoadingAreas[i].CheckCollisionWithPlayer(Player.Position);
            }
        }

        // Draws all of the boys
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calls Draw in GameScreen
            base.Draw(spriteBatch);

            // Just a little test thing
            for (int i = 0; i < LoadingAreas.Count; i++) LoadingAreas[i].Draw(spriteBatch);
        }

        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            base.DrawGUI(spriteBatch);

            int amount = 0;
            for (int i = 0; i < ParticleSystems.Count; i++)
            {
                amount += ParticleSystems[i].Particles.Count;
            }

            spriteBatch.DrawString(GameFonts.Arial, amount.ToString(), new Vector2(15, 46), Color.Black);
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

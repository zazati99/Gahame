﻿using System;
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
        List<ScreenLoadArea> loadingAreas;

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

            // TEEEST
            ct = new CutsceneTimer();
        }

        // Loads everything my dude
        public override void LoadContent()
        {
            base.LoadContent();
            // Load stuff and add GameObjects Below 

            // Test tiles
            Tileset tset = new Tileset(this);
            tset.TileAmount = new Vector2(4, 4);
            tset.LoadTexture("Backgrounds/tile");

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

            // test meme
            ParticleSystem sys = new ParticleSystem(this);
            sys.MinStartVelocity = new Vector2(-.5f, 0);
            sys.MaxStartVelocity = new Vector2(.5f, 0);
            sys.MinAcceleration = new Vector2(0, 0.1f);
            sys.MaxAcceleration = new Vector2(0, 0.15f);
            sys.MinScale = new Vector2(1, 1);
            sys.MaxScale = new Vector2(1, 4);

            sys.PositionOffset = new Vector2(Camera.View.X*.875f, 5);
            //sys.PositionOffset = new Vector2(ScreenSize.X / 2, 5);

            sys.LifeSpan = 90;

            sys.EmitAmount = (int)(150 * sys.PositionOffset.X * 2 / ScreenSize.X);
            //sys.EmitAmount = 350;

            sys.Position = new Vector2(ScreenSize.X/2, -20);

            Color[] data = new Color[1];
            data[0] = Color.DarkBlue;
            Texture2D meme = new Texture2D(Game1.Graphics.GraphicsDevice , 1, 1);
            meme.SetData(data);

            Color[] data2 = new Color[1];
            data2[0] = Color.Blue;
            Texture2D meme2 = new Texture2D(Game1.Graphics.GraphicsDevice, 1, 1);
            meme2.SetData(data2);

            sys.AddTexture(meme);
            sys.AddTexture(meme2);

            //sys.AddTexture(content.Load<Texture2D>("Sprites/Test"));

            sys.DestroyOnCollision = true;

            ParticleSystems.Add(sys);


            // test meme 2
            // test meme
            ParticleSystem sys2 = new ParticleSystem(this);
            sys2.MinStartVelocity = new Vector2(-.5f, -1);
            sys2.MaxStartVelocity = new Vector2(.5f, -1.5f);
            sys2.MinAcceleration = new Vector2(0, 0.1f);
            sys2.MaxAcceleration = new Vector2(0, 0.15f);
            sys2.MinScale = new Vector2(.3f, .3f);
            sys2.MaxScale = new Vector2(.85f, .85f);

            //sys.PositionOffset = new Vector2(ScreenSize.X / 2, 5);

            sys2.LifeSpan = 25;

            sys2.EmitAmount = 1;
            //sys.EmitAmount = 350;

            sys2.AddTexture(meme);
            sys2.AddTexture(meme2);

            //sys.AddTexture(content.Load<Texture2D>("Sprites/Test"));

            ParticleSystems.Add(sys2);
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

            ParticleSystems[0].Position.X = Player.Position.X;
            ParticleSystems[0].Emit();

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

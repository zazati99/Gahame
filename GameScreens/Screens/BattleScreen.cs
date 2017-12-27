using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class BattleScreen : GameScreen
    {
        // The previous GameScreen
        public GameScreen PreviousScreen;

        // Constructor
        public BattleScreen() : base()
        {
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
        }

        // Unloads all the trash
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        // Updates everything
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (GameControlls.Esc) EndBattle();
        }

        // Draws all of the boys
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        // Returns to previous screen
        public void EndBattle()
        {
            // Changes to previous screen and deletes this from memory;
            ScreenManager.Instance.ChangeScreenClear(PreviousScreen);
        }

    }
}

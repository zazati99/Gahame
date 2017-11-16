using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Gahame.GameObjects;
using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class GenericGameScreen : GameScreen 
    {

        // Constructor
        public GenericGameScreen() : base(){
            // Load content directly after being called
            LoadContent();
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

        }

        // Draws all of the boys
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;

namespace Gahame.GameObjects
{
    // Empty object (Only meant to be loaded)
    public class EmptyObject : GameObject
    {
        // Calls constructor
        public EmptyObject(GameScreen screen) : base(screen)
        {
            
        }
        // Should only be used when loading from file
        public EmptyObject() : base()
        {

        }

        // Initialize stuff
        public override void Initialize()
        {
  
        }

        // Updates the components that is being put in to this objeect
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // Draws components that is put in top this object
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects.ObjectComponents
{
    public class ObjectComponent
    {
        // lmao kolla om deet ens e möjligt att måla å så
        public bool Drawable { get; protected set; }
        public bool DrawableGUI { get; protected set; }
        public bool Updatable { get; protected set; }

        // Object that component exist in
        protected GameObject gameObject;

        // Stupid constructor
        public ObjectComponent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        // Updates component (if needed)
        public virtual void Update(GameTime gameTime)
        {

        }

        // Draws Component (if needed)
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        // Draws Gui (if needed)
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }
    }
}

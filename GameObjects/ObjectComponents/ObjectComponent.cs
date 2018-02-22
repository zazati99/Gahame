using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects.ObjectComponents
{
    public class ObjectComponent
    {
        // Object that component exist in
        public GameObject gameObject;

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

        // Set the GameObject of this component
        public void SetGameObject(GameObject o)
        {
            gameObject = o;
        }
    }
}

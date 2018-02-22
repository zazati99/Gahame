using Microsoft.Xna.Framework;

namespace Gahame.GameObjects.ObjectComponents
{
    public class OverworldDepthFix : ObjectComponent
    {
        // The sprite that the component will mess with
        Sprite objectSprite;

        // Constructor
        public OverworldDepthFix(GameObject gameObject) : base(gameObject)
        {

        }

        // Update
        public override void Update(GameTime gameTime)
        {
            // check if object has a sprite
            if (objectSprite == null)
            {
                objectSprite = gameObject.GetComponent<Sprite>();
            }

            // Fix depth
            if (objectSprite != null)
            {
                // Depth memes
                float depth = (0.3f / gameObject.Position.Y) + 0.5f;
                objectSprite.Depth = depth;
            }
        }
    }
}

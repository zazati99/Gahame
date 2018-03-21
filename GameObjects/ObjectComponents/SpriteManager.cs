using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects.ObjectComponents
{
    public class SpriteManager : ObjectComponent
    {
        // Sprite bois
        Dictionary<string, Sprite> sprites;

        // Key to current sprite
        public string CurrentSprite;

        // Constructor bois
        public SpriteManager(GameObject gameObject) : base(gameObject)
        {
            sprites = new Dictionary<string, Sprite>();
        }

        // Update current Sprite
        public override void Update(GameTime gameTime)
        {
            sprites[CurrentSprite].Update(gameTime);
        }

        // Draw current Sprite
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprites[CurrentSprite].Draw(spriteBatch);
        }

        // Change the current sprite
        public void ChangeSprite(string key)
        {
            CurrentSprite = key;
        }

        // Get sprite with key
        public Sprite GetSprite(string key)
        {
            return sprites[key];
        }

        // Does what you think xd
        public void ChangeScaleOnAllSprites(Vector2 scale)
        {
            foreach (KeyValuePair<string, Sprite> sprite in sprites)
            {
                sprite.Value.SpriteScale = scale;
            }
        }

        // Add a sprite
        public void AddSprite(string key, Sprite sprite)
        {
            // Add the sprite and key
            sprites.Add(key, sprite);

            // Change the current boy
            CurrentSprite = key;
        }

        // Add a sprite with path
        public void AddSprite(string key, string path)
        {
            // Create the sprite
            Sprite sprite = new Sprite(gameObject);
            sprite.AddImage(path);

            // Add that boy
            sprites.Add(key, sprite);

            // Change the current boy
            CurrentSprite = key;
        }
    }
}

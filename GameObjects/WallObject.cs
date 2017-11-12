using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{
    public class WallObject : GameObject
    {

        // Debug rectangle
        Texture2D rectangle;

        // Used when loaded form file
        public WallObject() : base()
        {
            Components.Add(new HitBox(this));
        }
        // Used when loaded form file
        public WallObject(Vector2 position, Vector2 Size, GameScreen screen) : base(screen)
        {
            Components.Add(new HitBox(this));
            Position = position;

            GetComponent<HitBox>().Colliders.Add(new BoxCollider(Size));
        }

        // Update components (should not have any)
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // Draws components(Should not have any byt could be used for debugging)
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (rectangle == null)
            {
                BoxCollider col = (BoxCollider)GetComponent<HitBox>().Colliders[0];
                rectangle = CreateRect(spriteBatch, col.Size);
            }

            spriteBatch.Draw(rectangle, Position);

        }

    }
}

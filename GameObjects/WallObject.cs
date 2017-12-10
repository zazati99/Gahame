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
        Sprite rectangle;

        // HitBox
        HitBox hb;

        // Used when loaded form file
        public WallObject() : base()
        {
            hb = new HitBox(this);
            hb.Solid = true;
            Components.Add(hb);
        }
        // Used when loaded form file
        public WallObject(Vector2 position, Vector2 Size, GameScreen screen) : base(screen)
        {
            HitBox hb = new HitBox(this);
            hb.Colliders.Add(new BoxCollider(Size));
            hb.Solid = true;
            Components.Add(hb);

            Position = position;
        }

        // Update components (should not have any)
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // Draws components(Should not have any byt could be used for debugging)
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (rectangle == null)
            {
                BoxCollider col = (BoxCollider)GetComponent<HitBox>().Colliders[0];
                rectangle = new Sprite(this);

                Texture2D tex = CreateRect(spriteBatch, col.Size);
                rectangle.AddImage(tex);

                rectangle.Depth = 1;

                Components.Add(rectangle);
            }

            base.Draw(spriteBatch);
        }

    }
}

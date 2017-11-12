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
using Gahame.GameScreens;

namespace Gahame.GameObjects.ObjectComponents.Colliders
{
    public class BoxCollider : Collider
    {

        // Size of the BoxCollider
        public Vector2 Size;

        // Constructor
        public BoxCollider()
        {
            Offset = new Vector2(0, 0);
        }
        public BoxCollider(Vector2 size)
        {
            Size = size;
            Offset = new Vector2(0, 0);
        }

        // Checks if this collider is within another collider
        public override bool IsColliding(Collider otherCol, Vector2 p1, Vector2 p2)
        {
            if (otherCol.GetType() == typeof(BoxCollider))
            {
                BoxCollider o = (BoxCollider)otherCol;
                return (p1.X+Offset.X < p2.X+o.Offset.X + o.Size.X &&
                    p1.X+Offset.X + Size.X > p2.X+o.Offset.X &&
                    p1.Y+Offset.Y < p2.Y+o.Offset.Y + o.Size.Y &&
                    p1.Y+Offset.Y + Size.Y > p2.Y+o.Offset.Y);
            }
            return false;
        }

    }
}

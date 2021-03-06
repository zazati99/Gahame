﻿using Microsoft.Xna.Framework;

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
        public override bool IsColliding(Collider otherCollider, Vector2 p1, Vector2 p2)
        {
              
            if (otherCollider is BoxCollider b)
            {
                return (p1.X + Offset.X < p2.X + b.Offset.X + b.Size.X &&
                p1.X + Offset.X + Size.X > p2.X + b.Offset.X &&
                p1.Y + Offset.Y < p2.Y + b.Offset.Y + b.Size.Y &&
                p1.Y + Offset.Y + Size.Y > p2.Y + b.Offset.Y);
            }

            return false;
        }

        public override bool IsCollidingWithPoint(Vector2 pos, Vector2 point)
        {
            return point.X > pos.X + Offset.X &&
                   point.X < pos.X + Size.X + Offset.X &&
                   point.Y > pos.Y + Offset.Y &&
                   point.Y < pos.Y + Size.Y + Offset.Y;
        }
    }
}

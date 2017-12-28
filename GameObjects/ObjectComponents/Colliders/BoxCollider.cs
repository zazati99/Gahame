using Microsoft.Xna.Framework;

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
            if (otherCol is BoxCollider o)
            {
                return (p1.X + Offset.X < p2.X + o.Offset.X + o.Size.X &&
                    p1.X + Offset.X + Size.X > p2.X + o.Offset.X &&
                    p1.Y + Offset.Y < p2.Y + o.Offset.Y + o.Size.Y &&
                    p1.Y + Offset.Y + Size.Y > p2.Y + o.Offset.Y);
            }
            return false;
        }
    }
}

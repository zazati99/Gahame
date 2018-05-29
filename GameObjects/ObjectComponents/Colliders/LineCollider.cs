using Microsoft.Xna.Framework;
using Gahame.GameUtils;

namespace Gahame.GameObjects.ObjectComponents.Colliders
{
    public class LineCollider : Collider
    {

        // End position of line
        public Vector2 EndPoint;

        public LineCollider(Vector2 endPoint) : base()
        {
            EndPoint = endPoint;
        }

        public bool DoesBoxIntersect(BoxCollider bc, Vector2 lineStart, Vector2 boxPosition)
        {
            LineCollider topRight = new LineCollider(new Vector2(boxPosition.X, boxPosition.Y));
            LineCollider BottomLeft = new LineCollider(new Vector2(boxPosition.X + bc.Size.X, boxPosition.Y + bc.Size.Y));

            if (DoesIntersect(topRight, lineStart, boxPosition + new Vector2(0, bc.Size.Y)))
                return true;

            if (DoesIntersect(topRight, lineStart, boxPosition + new Vector2(bc.Size.X, 0)))
                return true;

            if (DoesIntersect(BottomLeft, lineStart, boxPosition + new Vector2(bc.Size.X, 0)))
                return true;

            if (DoesIntersect(BottomLeft, lineStart, boxPosition + new Vector2(0, bc.Size.Y)))
                return true;

            return false; 
        }

        public Vector2 BoxIntersection(BoxCollider bc, Vector2 lineStart, Vector2 boxPosition)
        {
            LineCollider topRight = new LineCollider(new Vector2(boxPosition.X, boxPosition.Y));
            LineCollider BottomLeft = new LineCollider(new Vector2(boxPosition.X + bc.Size.X, boxPosition.Y + bc.Size.Y));

            Vector2 intersection = Vector2.Zero;
            if (DoesIntersect(topRight, lineStart, boxPosition + new Vector2(0, bc.Size.Y)))
                intersection = Intersection(topRight, lineStart, boxPosition + new Vector2(0, bc.Size.Y));

            if (DoesIntersect(topRight, lineStart, boxPosition + new Vector2(bc.Size.X, 0)))
            {
                Vector2 tempIntersection = Intersection(topRight, lineStart, boxPosition + new Vector2(bc.Size.X, 0));
                if (MyMaths.DistanceCubed(lineStart, tempIntersection) > MyMaths.DistanceCubed(lineStart, intersection)) intersection = tempIntersection;
            }

            if (DoesIntersect(BottomLeft, lineStart, boxPosition + new Vector2(bc.Size.X, 0)))
            {
                Vector2 tempIntersection = Intersection(BottomLeft, lineStart, boxPosition + new Vector2(bc.Size.X, 0));
                if (MyMaths.DistanceCubed(lineStart, tempIntersection) > MyMaths.DistanceCubed(lineStart, intersection)) intersection = tempIntersection;
            }

            if (DoesIntersect(BottomLeft, lineStart, boxPosition + new Vector2(0, bc.Size.Y)))
            {
                Vector2 tempIntersection = Intersection(BottomLeft, lineStart, boxPosition + new Vector2(0, bc.Size.Y));
                if (MyMaths.DistanceCubed(lineStart, tempIntersection) > MyMaths.DistanceCubed(lineStart, intersection)) intersection = tempIntersection;
            }

            return intersection;
        }

        public Vector2 Intersection(LineCollider otherLine, Vector2 p1, Vector2 p2)
        {

            float k1 = (EndPoint.Y - p1.Y) / (EndPoint.X - p1.X);
            float k2 = (otherLine.EndPoint.Y - p2.Y) / (otherLine.EndPoint.X - p2.X);

            float xIntersect = (p2.Y - p1.Y) / (k1 - k2);
            float yIntersect = k1 * xIntersect + p1.Y;

            return new Vector2(xIntersect, yIntersect);

        }

        public bool DoesIntersect(LineCollider otherLine, Vector2 p1, Vector2 p2)
        {
            float k1 = (EndPoint.Y - p1.Y) / (EndPoint.X - p1.X);
            float k2 = (otherLine.EndPoint.Y - p2.Y) / (otherLine.EndPoint.X - p2.X);

            if (k1 == k2) return false;

            float xIntersect = (p2.Y - p1.Y) / (k1 - k2);
            float yIntersect = k1 * xIntersect + p1.Y;

            return (MyMaths.DistanceCubed(p1, EndPoint) < MyMaths.DistanceCubed(p1, new Vector2(xIntersect, yIntersect)));
        }

    }
}

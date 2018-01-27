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
    public class Collider
    {

        // offset from GameObject
        public Vector2 Offset;

        // Virtual function that will be overriden
        public virtual bool IsColliding(Collider otherCol, Vector2 p1, Vector2 p2)
        {
            return false;
        }

        // Virtual function that checks if colliding with certain point
        public virtual bool IsCollidingWithPoint(Vector2 pos, Vector2 point)
        {
            return false;
        }

    }
}

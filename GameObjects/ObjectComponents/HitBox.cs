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

using Gahame.GameObjects.ObjectComponents.Colliders;

namespace Gahame.GameObjects.ObjectComponents
{
    public class HitBox : ObjectComponent
    {

        // check to see if you can walk throug this hitbox
        public bool Solid;

        // List of Colliders containing in the hitbox
        public List<Collider> Colliders;

        // constructor that doesnt create finnished collider
        public HitBox(GameObject gameObject) : base(gameObject)
        {
            initialize();
        }
        // Constructor for creating hitbox with finnished collider
        public HitBox(Vector2 size, GameObject gameObject) : base(gameObject)
        {
            initialize();
            Colliders.Add(new BoxCollider(new Vector2(64,64)));
        }

        // Initialize Should be called in constructor
        void initialize()
        {
            //updatable and drawable things
            Updatable = false;
            Drawable = false;
            // Creating collider stuff
            Colliders = new List<Collider>();
            Solid = false;
        }

        // Check if 2 hitboxes at a given position is colliding (By type)
        public bool PlaceMeeting<T>(Vector2 p)
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.screen.GameObjects[i];
                if (o == gameObject) continue;

                HitBox temp = o.GetComponent<HitBox>();
                if (o.GetType() == typeof(T) && temp != null)
                {
                    for (int j = 0; j < Colliders.Count; j++)
                    {
                        for (int k = 0; k < temp.Colliders.Count; k++)
                        {
                            if (Colliders[j].IsColliding(temp.Colliders[k], p, o.Position))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        // Check if 2 hitboxes at a given position is colliding By type
        public bool PlaceMeeting(Vector2 p, Type type)
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.screen.GameObjects[i];
                if (o == gameObject) continue;

                HitBox temp = o.GetComponent<HitBox>();
                if (o.GetType() == type && temp != null)
                {
                    for (int j = 0; j < Colliders.Count; j++)
                    {
                        for (int k = 0; k < temp.Colliders.Count; k++)
                        {
                            if (Colliders[j].IsColliding(temp.Colliders[k], p, o.Position))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        // Check if 2 hitboxes at a given position is colliding (By Tag)
        public bool PlaceMeeting(Vector2 p, string tag)
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.screen.GameObjects[i];
                if (o == gameObject) continue;

                if (o.Tag == tag)
                {
                    HitBox otherHb = o.GetComponent<HitBox>();
                    if (otherHb != null)
                    {
                        for (int j = 0; j < Colliders.Count; j++)
                        {
                            for (int k = 0; k < otherHb.Colliders.Count; k++)
                            {
                                if (Colliders[j].IsColliding(otherHb.Colliders[k], p, o.Position))
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        // Check if 2 hitboxes at a given position is colliding (By solid)
        public bool SolidMeeting(float x, float y)
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.screen.GameObjects[i];
                if (o == gameObject) continue;

                HitBox temp = o.GetComponent<HitBox>();
                if (temp != null)
                {
                    if (temp.Solid)
                    {
                        for (int j = 0; j < Colliders.Count; j++)
                        {
                            for (int k = 0; k < temp.Colliders.Count; k++)
                            {
                                if (Colliders[j].IsColliding(temp.Colliders[k], new Vector2(x, y), o.Position))
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public GameObject InstancePlace(Vector2 pos, string tag)
        {
            GameObject obj;

            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                obj = gameObject.screen.GameObjects[i];
                if (obj == gameObject) continue;

                if (obj.Tag == tag)
                {
                    HitBox otherHb = obj.GetComponent<HitBox>();
                    if (otherHb != null) {
                        for (int j = 0; j < Colliders.Count; j++)
                        {
                            for (int k = 0; k < otherHb.Colliders.Count; k++)
                            {
                                if (Colliders[j].IsColliding(otherHb.Colliders[k], pos, obj.Position))
                                    return obj;
                            }
                        }
                    }
                }
            }

            return null;
        }

    }
}

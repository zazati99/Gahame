using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Gahame.GameObjects.ObjectComponents.DialogueSystem;
using Gahame.GameObjects.ObjectComponents.Colliders;

namespace Gahame.GameObjects.ObjectComponents
{
    public class HitBox : ObjectComponent
    {

        // check to see if you can walk throug this hitbox
        public bool Solid;

        // List of Colliders containing in the hitbox
        public List<Collider> Colliders;

        // Priority thing
        public short Priority;

        // constructor that doesnt create finnished collider
        public HitBox(GameObject gameObject) : base(gameObject)
        {
            initialize();
        }
        // Constructor for creating hitbox with finnished collider
        public HitBox(Vector2 size, GameObject gameObject) : base(gameObject)
        {
            initialize();
            Colliders.Add(new BoxCollider(size));
        }

        // Initialize Should be called in constructor
        void initialize()
        {
            // Creating collider stuff
            Colliders = new List<Collider>();
            Solid = false;

            Priority = 1;
        }

        // Check if 2 hitboxes at a given position is colliding (By type)
        public bool PlaceMeeting<T>(Vector2 p)
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.screen.GameObjects[i];
                if (o == gameObject) continue;

                HitBox temp = o.GetComponent<HitBox>();
                if (o is T && temp != null)
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

        // Check if colliding with object that has dialogue 
        public Dialogue DialogueMeeting(Vector2 pos)
        {
            GameObject obj;

            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                obj = gameObject.screen.GameObjects[i];
                if (obj == gameObject) continue;

                Dialogue d = obj.GetComponent<Dialogue>();

                if (d != null)
                {
                    if (d.Accesible)
                    {
                        HitBox otherHb = obj.GetComponent<HitBox>();
                        if (otherHb != null)
                        {
                            for (int j = 0; j < Colliders.Count; j++)
                            {
                                for (int k = 0; k < otherHb.Colliders.Count; k++)
                                {
                                    if (Colliders[j].IsColliding(otherHb.Colliders[k], pos, obj.Position))
                                        return d;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Gets the object that is colliding at a specific place
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

        // Gets the solid object at a specific place
        public HitBox SolidPlace(Vector2 pos)
        {
            GameObject obj;
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                obj = gameObject.screen.GameObjects[i];
                if (obj == gameObject) continue;

                HitBox s = obj.GetComponent<HitBox>();
                if (s != null)
                {
                    if (s.Solid)
                    {
                        for (int j = 0; j < Colliders.Count; j++)
                        {
                            for (int k = 0; k < s.Colliders.Count; k++)
                            {
                                if (Colliders[j].IsColliding(s.Colliders[k], pos, obj.Position))
                                    return s;
                            }
                        }
                    }
                }
            }

            return null;
        }

        // Gets the object that is colliding at a specific place
        public GameObject InstancePlace<T>(Vector2 pos)
        {
            GameObject obj;

            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                obj = gameObject.screen.GameObjects[i];
                if (obj == gameObject) continue;

                if (obj is T)
                {
                    HitBox otherHb = obj.GetComponent<HitBox>();
                    if (otherHb != null)
                    {
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

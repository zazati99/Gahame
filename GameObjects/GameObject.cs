using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;

namespace Gahame.GameObjects
{
    public class GameObject
    {
        // Object components
        public List<ObjectComponent> Components;

        // The Screen that the object is in
        public GameScreen screen;

        // Position of object
        public Vector2 Position;

        // Tag to differetiate between dirrefent GameObjects
        public string Tag;

        // Normal constructor
        public GameObject(GameScreen screen)
        {
            // Creates inmportant variables
            Components = new List<ObjectComponent>();
            this.screen = screen;
        }
        // Constructor that only shoud be used when loading file
        public GameObject()
        {
            // Creates inmportant variables
            Components = new List<ObjectComponent>();
        }

        // Initialize stuff, put initialization that need screen here if  you gonna be loading it from file
        public virtual void Initialize()
        {

        }

        // UNloads content
        public virtual void UnloadContent()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] is IDisposable d)
                {
                    d.Dispose();
                }
            }
        }

        // Updates GameObject (Must be base thing)
        public virtual void Update(GameTime gameTime)
        {
            // Updates the components of the object (if it even can)
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Update(gameTime);
            }
        }

        // Draws GameObject (must be base thing)
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draws the components (if posiiblö)
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Draw(spriteBatch);
            }
        }

        // Draws GUI for Game Objects
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {
            // Draws the GUI (if posiiblö)
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].DrawGUI(spriteBatch);
            }
        }

        #region Nice Game Functions

        // Get desired Component
        public T GetComponent<T>()
        {
            object instance = null;
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] is T)
                {
                    instance = Components[i];
                    return (T)instance;
                }
            }
            return (T)instance;
        }

        // Change the current Sprite (Horrible, use sprteManager instead)
        protected void ChangeSprite(Sprite newSprite)
        {
            Components.Remove(GetComponent<Sprite>());
            Components.Add(newSprite);
        }

        // PlaceMeeting with vector
        public bool PlaceMeeting<T>(Vector2 p)
        {
            HitBox hb = GetComponent<HitBox>();
            return hb != null && hb.PlaceMeeting<T>(p);
        }

        // PlaceMeeting with X and Y coordinates
        public bool PlaceMeeting<T>(float x, float y)
        {
            HitBox hb = GetComponent<HitBox>();
            return hb != null && hb.PlaceMeeting<T>(new Vector2(x, y));
        }

        // PlaceMeeting with vector With specific tag
        public bool PlaceMeeting(Vector2 p, string tag)
        {
            HitBox hb = GetComponent<HitBox>();
            return hb != null && hb.PlaceMeeting(p, tag);
        }

        // Destroy a GameObject
        protected void DestroyObject(GameObject o)
        {
            o.UnloadContent();
            screen.GameObjects.Remove(o);
        }
        // Destroy this GameObject
        public void DestroyObject()
        {
            UnloadContent();
            screen.GameObjects.Remove(this);
        }

        // Texture stuff
        protected Texture2D CreateRect(Vector2 size)
        {
            Random r = new Random(GahameController.Seed);
            Texture2D rect = new Texture2D(Game1.Graphics.GraphicsDevice, (int)size.X, (int)size.Y);

            int variation = 16;

            Color[] data = new Color[(int)size.X * (int)size.Y];

            Color mainColor = new Color(r.Next() % 255, r.Next() % 255, r.Next() % 255);


            data[0] = mainColor;

            for (int i = 1; i < data.Length; i++)
            {
                int newR = 0;
                int newG = 0;
                int newB = 0;

                //Add variation
                if (i % size.X == 0)
                {
                    newR = data[i % (int)(size.X)].R + r.Next() % 2 * variation + 1 - variation;
                    newG = data[i % (int)(size.X)].G + r.Next() % 2 * variation + 1 - variation;
                    newB = data[i % (int)(size.X)].B + r.Next() % 2 * variation + 1 - variation;
                }
                else if (i > size.X)
                {
                    newR = (data[i - 1].R + data[i % (int)size.X].R) / 2 + r.Next() % 2 * variation + 1 - variation;
                    newG = (data[i - 1].G + data[i % (int)size.X].G) / 2 + r.Next() % 2 * variation + 1 - variation;
                    newB = (data[i - 1].B + data[i % (int)size.X].B) / 2 + r.Next() % 2 * variation + 1 - variation;
                } else
                {
                    newR = data[i - 1].R + r.Next() % 2 * variation + 1 - variation;
                    newG = data[i - 1].G + r.Next() % 2 * variation + 1 - variation;
                    newB = data[i - 1].B + r.Next() % 2 * variation + 1 - variation;
                }

                //Keep it from memeing too far, add "weight"
                newR += (int)((mainColor.R - newR)/8);
                newG += (int)((mainColor.G - newG)/8);
                newB += (int)((mainColor.B - newB)/8);

                //Stop keep it in
                newR = Math.Max(Math.Min(newR, 255), 0);
                newG = Math.Max(Math.Min(newG, 255), 0);
                newB = Math.Max(Math.Min(newB, 255), 0);
                data[i] = new Color(newR, newG, newB);
            }

            rect.SetData(data);

            return rect;
        }

        #endregion
    }
}

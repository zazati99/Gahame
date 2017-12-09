﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Gahame;
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

        // Initialize stuff
        public virtual void Initialize()
        {

        }

        // Updates GameObject (Must be base thing)
        public virtual void Update(GameTime gameTime)
        {
            // Updates the components of the object (if it even can)
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Updatable)
                    Components[i].Update(gameTime);
            }
            
        }

        // Draws GameObject (must be base thing)
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draws the components (if posiiblö)
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Drawable)
                    Components[i].Draw(spriteBatch);
            }
        }


        // Good Game Functions

        // Get desired Component
        public T GetComponent<T>()
        {
            object instance = null;
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].GetType() == typeof(T))
                {
                    instance = Components[i];
                }
            }
            return (T)instance;
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
        public bool PlaceMeeting(Vector2 p, Type type)
        {
            HitBox hb = GetComponent<HitBox>();
            return hb != null && hb.PlaceMeeting(p, type);
        }
        public bool PlaceMeeting(float x, float y, Type type)
        {
            HitBox hb = GetComponent<HitBox>();
            return hb != null && hb.PlaceMeeting(new Vector2(x, y), type);
        }

        // Destroy a GameObject
        protected void destroyObject(GameObject o)
        {
            screen.GameObjects.Remove(o);
        }

        // Texture stuff
        protected Texture2D CreateRect(SpriteBatch spriteBatch, Vector2 size)
        {
            Random r = new Random();
            Texture2D rect = new Texture2D(spriteBatch.GraphicsDevice, (int)size.X, (int)size.Y);

            int variation = 2;


            Color[] data = new Color[(int)size.X * (int)size.Y];

            Color mainColor = new Color(50, 5, 5);
            data[0] = mainColor;

            for (int i = 1; i < data.Length; i++)
            {

                //Add variation
                int newR = data[i - 1].R + r.Next() % 2 * variation + 1 - variation;
                int newG = data[i - 1].G + r.Next() % 2 * variation + 1 - variation;
                int newB = data[i - 1].B + r.Next() % 2 * variation + 1 - variation;

                //Keep it from memeing too far, add "weight"
                newR += (int)((mainColor.R - newR) / 20);
                newR += (int)((mainColor.R - newR) / 20);
                newR += (int)((mainColor.R - newR) / 20);

                //Stop keep it in
                newR = Math.Max(Math.Min(newR, 255), 0);
                newG = Math.Max(Math.Min(newG, 255), 0);
                newB = Math.Max(Math.Min(newB, 255), 0);
                data[i] = new Color(newR, newG, newB);
            }

            rect.SetData(data);

            return rect;
        }




    }
}

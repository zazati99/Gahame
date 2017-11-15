using System;
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

        // Destroy a GameObject
        protected void destroyObject(GameObject o)
        {
            screen.GameObjects.Remove(o);
        }

        // Approach a value
        protected float approach(float value, float target, float speed)
        {
            if (value == target) return target;
            
            if (value < target)
            {
                value += speed;
                if (value > target) return target;
            }  else
            {
                value -= speed;
                if (value < target) return target;
            }
            return value;
        }

        // Maxes out a value
        protected float max(float value, float max)
        {
            if (value > max) return max;
            return value;
        }
        // makes value small thing out of a value
        protected float min(float value, float min)
        {
            if (value < min) return min;
            return value;
        }

        protected int signum(float n){
            if (n < 0) return -1;
            if (n > 0) return 1;
            return 0;
        }

        protected float lerpFloat(float value1, float value2, float amount){
            return value1 + (value2 - value1) * amount;
        }

        // Creates a rectangle and puts it in a texture (should only be used for debugging)
        protected Texture2D CreateRect(SpriteBatch spriteBatch, Vector2 size)
        {
            Random r = new Random();

            Texture2D rect = new Texture2D(spriteBatch.GraphicsDevice, (int)size.X, (int)size.Y);
            Color[] data = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < data.Length; i++) data[i] = new Color(r.Next()*255,r.Next()*255,r.Next()*255);
            rect.SetData(data);

            return rect;
        }

    }
}

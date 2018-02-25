using NLua;

using System;

namespace Gahame.GameObjects.ObjectComponents
{
    public class LuaScript : ObjectComponent, IDisposable
    {
        // Lua thing
        Lua lua;

        // Delete dis
        bool willDelete;

        // needed constructor
        public LuaScript(GameObject gameObject) : base(gameObject)
        {
            lua = new Lua();
            willDelete = false;
        }

        // Initializes lua script
        public virtual void InitializeLua(string luaScript)
        {
            // Register functions
            lua.RegisterFunction("instanceDestroy", this, GetType().GetMethod("instanceDestroy"));

            setFloat("x", gameObject.Position.X);
            setFloat("y", gameObject.Position.Y);
            setFloat("xSpeed", gameObject.GetComponent<Physics>().Velocity.X);
            setFloat("ySpeed", gameObject.GetComponent<Physics>().Velocity.Y);

            lua.DoString(luaScript);
            lua.GetFunction("Start").Call();
        }

        // Runs update function in script
        public override void Update(Microsoft.Xna.Framework.GameTime  gameTime)
        {
            setFloat("x", gameObject.Position.X);
            setFloat("y", gameObject.Position.Y);
            setFloat("xSpeed", gameObject.GetComponent<Physics>().Velocity.X);
            setFloat("ySpeed", gameObject.GetComponent<Physics>().Velocity.Y);

            lua.GetFunction("Update").Call();

            gameObject.Position.X = getFloat("x");
            gameObject.Position.Y = getFloat("y");
            gameObject.GetComponent<Physics>().Velocity.X = getFloat("xSpeed");
            gameObject.GetComponent<Physics>().Velocity.Y = getFloat("ySpeed");

            if (willDelete)
            {
                gameObject.DestroyObject();
            }
        }

        // Lua set variable
        void setFloat(string variable, float f)
        {
            lua[variable] = f;
        }

        // Get float
        float getFloat(string variable)
        {
            return (float)(double)lua[variable];
        }

        // Dispose some scipt
        public void Dispose()
        {
            // KIll lua
            lua.Close();
            lua.Dispose();
        }

        // DEstroy gameObject
        public void instanceDestroy()
        {
            willDelete = true;
        }
    }
}

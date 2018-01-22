using NLua;

using Gahame.GameUtils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gahame.GameObjects.ObjectComponents
{
    public class LuaScript : ObjectComponent, IDisposable
    {
        // Lua thing
        Lua lua;

        // needed constructor
        public LuaScript(GameObject gameObject) : base(gameObject)
        {
            // HOHOHH
            Updatable = true;
            //lua = new Lua();
            lua = new Lua();
            

            //lua.LoadCLRPackage();
        }

        // Initializes lua script
        public void InitializeLua(string luaScript)
        {
            // Register functions
            lua.RegisterFunction("instanceDestroy", this, GetType().GetMethod("instanceDestroy"));

            lua.DoString(luaScript);
            lua.DoString("Start()");
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

            if (isGameObjectDelete()) gameObject.UnloadContent();
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

        public void instanceDestroy()
        {
            gameObject.screen.GameObjects.Remove(gameObject);
        }

        bool isGameObjectDelete()
        {
            for (int i = 0; i < gameObject.screen.GameObjects.Count; i++)
            {
                if (gameObject.screen.GameObjects[i] == gameObject)
                    return false;
            }
            return true;
        }

    }
}

using NLua;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gahame.GameObjects.ObjectComponents
{
    public class LuaScript : ObjectComponent
    {
        // Lua thing
        Lua lua;

        // Bindings
        List<float> bindingCS;
        List<string> bindingLua;

        // needed constructor
        public LuaScript(GameObject gameObject) : base(gameObject)
        {
            // HOHOHH
            Updatable = true;
            lua = new Lua();

            bindingCS = new List<float>();
            bindingLua = new List<string>();

            addFloatBindings(ref gameObject.Position.X, "x");
            addFloatBindings(ref gameObject.Position.Y, "y");
            addFloatBindings(ref gameObject.GetComponent<Physics>().Velocity.X, "xSpeed");
            addFloatBindings(ref gameObject.GetComponent<Physics>().Velocity.Y, "ySpeed");
        }

        // Initializes lua script
        public void InitializeLua(string luaScript)
        {
            lua.DoString(luaScript);
            lua.DoString("Start()");
        }

        // Runs update function in script
        public override void Update(Microsoft.Xna.Framework.GameTime  gameTime)
        {
            setBindings();

            lua.DoString("Update()");

            getBindings();
        }

        void addFloatBindings(ref float f, string s)
        {
            bindingCS.Add(f);
            bindingLua.Add(s);
        }

        void setBindings()
        {
            for (int i = 0; i < bindingCS.Count; i++)
            {
                lua[bindingLua[i]] = bindingCS[i];   
            }
        }

        void getBindings()
        {
            for (int i = 0; i < bindingCS.Count; i++)
            {
                bindingCS[i] = (float)(double)lua[bindingLua[i]];
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
    }
}

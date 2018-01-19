using NLua;

namespace Gahame.GameObjects.ObjectComponents
{
    public class LuaScript : ObjectComponent
    {
        // Lua thing
        Lua lua;

        // needed constructor
        public LuaScript(GameObject gameObject) : base(gameObject)
        {
            // HOHOHH
            Updatable = true;
            lua = new Lua();
            lua.LoadCLRPackage();
        }

        // Initializes lua script
        public void InitializeLua(string luaScript)
        {
            lua.DoString(luaScript);
            lua.DoString("Start()");
        }

        // Runs update function in script
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            setFloat("X", gameObject.Position.X);
            setFloat("Y", gameObject.Position.Y);

            lua.DoString("Update()");

            gameObject.Position.X = getFloat("X");
            gameObject.Position.Y = getFloat("Y");
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

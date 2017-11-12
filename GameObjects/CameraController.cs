using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gahame.GameScreens;

using Microsoft.Xna.Framework;

namespace Gahame.GameObjects
{
    public class CameraController
    {

        // target that camera will be following my dude
        public GameObject target;

        // Moving camer?
        public bool Static;

        // Camera constructorino
        public CameraController(GameObject o)
        {
            this.target = o;
            Static = false;
        }

        // transform position
        public void Transform(Vector2 pos)
        {
            Game1.cam.Move(pos);
        }

        // Set position of le camera
        public void SetPosition(Vector2 pos)
        {
            Game1.cam.Pos = pos;
        }

        // Updates the camera
        public void Update()
        {
            if (Static == false)
            {
                if (target != null)
                {
                    //SetPosition(Vector2.Add(target.Position, new Vector2(-320, -180)));
                    SetPosition(Vector2.Lerp(Game1.cam.Pos, Vector2.Add(target.Position, new Vector2(-ScreenManager.DefaultViewportX/4, -ScreenManager.DefaultViewportY / 4)), .1f));
                }
            }
        }

    }
}

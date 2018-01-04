using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class ScreenTransitionRectangle : ScreenTransition
    {
        Vector2 rec1;
        Vector2 rec2;
        Vector2 rec3;
        Vector2 rec4;

        Vector2 rec5;
        Vector2 rec6;

        Vector2 size;
        Vector2 size2;

        bool screenChanged;

        // EMpty constructor best constructor
        public ScreenTransitionRectangle(GameScreen currentScreen, GameScreen newScreen, bool clearOldScreen) : base(currentScreen, newScreen, clearOldScreen)
        {

        }

        // Start override
        public override void StartTransition()
        {
            // Base thing
            base.StartTransition();

            // is screen changeed? ofcourse not
            screenChanged = false;

            // Size
            size = new Vector2(Camera.View.X, Camera.View.Y / 4);
            size2 = new Vector2(Camera.View.X / 2, Camera.View.Y);

            // Positions
            rec1 = new Vector2(-size.X, 0);
            rec2 = new Vector2(size.X, size.Y);
            rec3 = new Vector2(-size.X, size.Y * 2);
            rec4 = new Vector2(size.X, size.Y * 3);

            rec5 = new Vector2(0, 0);
            rec6 = new Vector2(size2.X, 0);
        }

        // Updateroni
        public override void Update(GameTime gameTime)
        {
            // Inportant fix
            size = new Vector2(Camera.View.X, Camera.View.Y / 4);
            size2 = new Vector2(Camera.View.X / 2, Camera.View.Y);

            // Y Position fix
            rec1.Y = 0;
            rec2.Y = size.Y;
            rec3.Y = size.Y * 2;
            rec4.Y = size.Y * 3;

            // rec 1 thing
            rec1.X = MyMaths.Lerp(rec1.X, 0, .25f);

            // rec 2 thing
            if ((int)rec1.X == 0)
            {
                rec4.X = MyMaths.Lerp(rec4.X, 0, .25f);
            }

            // rec 3 and 4 ok
            if ((int)rec4.X == 0)
            {
                rec2.X = MyMaths.Lerp(rec2.X, 0, .25f);
                rec3.X = MyMaths.Lerp(rec3.X, 0, .25f);
            }

            // rec 5 n 6
            if ((int)rec2.X == 0)
            {
                if (!screenChanged)
                {
                    ChangeScreen();
                    screenChanged = true;
                }
                rec5.X = MyMaths.Lerp(rec5.X, -size2.X, .25f);
                rec6.X = MyMaths.Lerp(rec6.X, size.X, .25f);
            }

            // STOPIT
            if ((int)(rec6.X+.5f) == size.X)
            {
                StopTransition();
            }

        }

        // Draweroni
        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            // Draw ze rectangles
            if ((int)rec2.X != 0)
            {
                ShapeRenderer.FillRectangle(spriteBatch, rec1, size, Color.Black);
                ShapeRenderer.FillRectangle(spriteBatch, rec2, size, Color.Black);
                ShapeRenderer.FillRectangle(spriteBatch, rec3, size, Color.Black);
                ShapeRenderer.FillRectangle(spriteBatch, rec4, size, Color.Black);
            } else
            {
                ShapeRenderer.FillRectangle(spriteBatch, rec5, size2, Color.Black);
                ShapeRenderer.FillRectangle(spriteBatch, rec6, size2, Color.Black);
            }
            
        }
    }
}

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

        float speed1, speed2, speed4;

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

            speed1 = 25;
            speed2 = 25;
            speed4 = 25;
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
            speed1 = MyMaths.Approach(speed1, 1, 0.95f);
            rec1.X = MyMaths.Approach(rec1.X, 0, speed1);

            // rec 2 thing
            if (rec1.X == 0)
            {
                speed4 = MyMaths.Approach(speed4, 1, 0.95f);
                rec4.X = MyMaths.Approach(rec4.X, 0, speed4);
            }

            // rec 3 and 4 ok
            if (rec4.X == 0)
            {
                speed2 = MyMaths.Approach(speed2, 1, 0.95f);
                rec2.X = MyMaths.Approach(rec2.X, 0, speed2);
                rec3.X = MyMaths.Approach(rec3.X, 0, speed2);
            }

            // rec 5 n 6
            if (rec2.X == 0)
            {
                if (!screenChanged)
                {
                    ChangeScreen();
                    screenChanged = true;
                }
                rec5.X = MyMaths.Approach(rec5.X, -size2.X, 5);
                rec6.X = MyMaths.Approach(rec6.X, size.X, 5);
            }

            // STOPIT
            if (rec6.X == size.X)
            {
                StopTransition();
            }

        }

        // Draweroni
        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            // Draw ze rectangles
            if (rec2.X != 0)
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

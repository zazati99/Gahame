using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class ScreenTransitionFade : ScreenTransition
    {
        // Rectangle meme
        Rectangle rect;

        // float alpha
        float alpha;

        // bool go Back
        bool reverseAlpha;

        float fadeSpeed;

        // Siiic constructor mate
        public ScreenTransitionFade(float fadeSpeed ,GameScreen current, GameScreen next, bool clearOldScreen) : base(current, next, clearOldScreen)
        {
            this.fadeSpeed = fadeSpeed;
        }

        public override void StartTransition()
        {
            // run start tansition in base thing
            base.StartTransition();

            // creates fadeing rectangle
            rect = new Rectangle(0, 0, (int)Camera.View.X, (int)Camera.View.Y);

            // COlor of rect
            alpha = 0;

            // go back
            reverseAlpha = false;
        }

        // Update 
        public override void Update(GameTime gameTime)
        {
            alpha = MyMaths.Approach(alpha, (reverseAlpha ? 0 : 1), fadeSpeed);
            if (alpha >= 1)
            {
                reverseAlpha = true;
                ChangeScreen();
            }
            if (alpha == 0)
            {
                StopTransition();
            }
        }

        // Draw the rectnagel
        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            ShapeRenderer.FillRectangle(spriteBatch, rect, new Color(0, 0, 0, alpha));
        }
    }
}

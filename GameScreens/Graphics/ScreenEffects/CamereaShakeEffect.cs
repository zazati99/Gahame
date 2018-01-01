using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Gahame.GameUtils;

namespace Gahame.GameScreens
{
    public class CameraShakeEffect  : ScreenEffect
    {
        // The timer
        Timer timer;

        // intensity of shake
        float intensity;

        // creates random position values
        Random r;

        // intensity deplete constant
        float deplete;

        // screen that it can steeal things from
        GameScreen screen;

        // Constructor
        public CameraShakeEffect(GameScreen screen, float intensity, int timeInFrames)
        {
            // Sets varables
            timer = new Timer(timeInFrames, true);
            this.intensity = intensity;
            this.screen = screen;

            // Creates constant variables
            deplete = intensity / timeInFrames;
            r = new Random();
        }

        // Update, does all of the shaking
        public override void Update(GameTime gameTime)
        {
            if (!timer.Check())
            {
                // random position
                Vector2 randomPos = new Vector2(r.Next(-(int)(intensity * intensity) / 2, (int)(intensity * intensity)) / 2, r.Next(-(int)(intensity * intensity) / 2, (int)(intensity * intensity) / 2));
                screen.CamController.Position += randomPos;

                // random rotation
                float newRotation = (float)RandomNumberBetween(-(intensity * intensity) / 2, intensity * intensity / 2)/125;
                screen.CamController.Rotation += newRotation;

                // lerp back rotation
                screen.CamController.Rotation = MyMaths.Lerp(screen.CamController.Rotation, 0, .25f);

                // rumble boy
                if (GameInput.ControllerMode)
                    GamePad.SetVibration(0, .25f, .25f);

                // deplete the intensity
                intensity -= deplete;
            } else
            {
                // set stuff back to normal and remove this
                screen.CamController.Rotation = 0;
                GamePad.SetVibration(0, 0, 0);
                screen.ScreenEffects.Remove(this);
            }
        }

        // random float thing
        double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = r.NextDouble();
            return minValue + (next * (maxValue - minValue));
        }

    }
}

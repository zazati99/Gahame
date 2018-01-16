using System;

using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{

    // Overworld version of player object
    public class PlayerObjectOverworld : PlayerObject
    {
        // Componenst
        Sprite sprite;
        HitBox hitBox;
        Physics physics;

        // Controlls variablees
        float maxSpeed;
        float accelerationSpeed;
        float slowDownSpeed;

        // normalized vector
        Vector2 norm;

        string TextTest;

        // Constructor stufferoo for playerino
        public PlayerObjectOverworld(GameScreen screen) : base(screen)
        {
            // Cool sprite stuff
            sprite = new Sprite(this);
            sprite.Depth = .1f;
            sprite.AddImage("Sprites/Player/playerBattleScreenStill");
            sprite.SpriteOrigin = new Vector2(24, 24);
            Components.Add(sprite);

            // HitBox COmponent 
            hitBox = new HitBox(this);
            hitBox.Colliders.Add(new BoxCollider(new Vector2(14, 13)));
            hitBox.Colliders[0].Offset.X = -7;
            hitBox.Colliders[0].Offset.Y = 11;
            Components.Add(hitBox);

            // Physics
            physics = new Physics(this);
            physics.Solid = true;
            physics.GravityEnabled = false;
            Components.Add(physics);

            // Camera
            screen.CamController.Target = this;
            screen.CamController.MovementAmount = new Vector2(.2f, .2f);

            // Depth fix
            Components.Add(new OverworldDepthFix(this));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            slowDownSpeed = .25f;

            TextTest = "";

        }

        // Update player
        public override void Update(GameTime gameTime)
        {
            // Do start update things
            StartUpdate();

            // Normalized vector
            if (GameInput.ControllerMode)
            {
                //norm = MyMaths.Normalize(GameControlls.LeftStickX, GameControlls.LeftStickY);
                norm.X = GameInput.LeftStickX;
                norm.Y = GameInput.LeftStickY;
                if (Math.Abs(norm.X) > 0.70f || Math.Abs(norm.Y) > 0.70f)
                    norm = MyMaths.Normalize(GameInput.LeftStickX, GameInput.LeftStickY);
            } else
            {
                norm = MyMaths.Normalize((GameInput.RightCD ? 1 : 0) - (GameInput.LeftCD ? 1 : 0), (GameInput.DownCD ? 1 : 0) - (GameInput.UpCD ? 1 : 0));
            }

            // Do movement if no cutscene
            if (!GahameController.CutScene)
            {
                // Horizontal speed
                if (GameInput.RightCD || GameInput.LeftCD)
                {
                    // le nice walk horizontal thing
                    WalkHorizontal(maxSpeed * norm.X);
                }

                // Vertical speed
                if (GameInput.UpCD || GameInput.DownCD)
                {
                    // le nice walk horizontal thing
                    WalkVertical(maxSpeed * norm.Y);
                }
            }

            // Activate Object
            if (GameInput.ActivateCD)
            {
                // Insane
                if (hitBox.InstancePlace<ActivatableObject>(Position) is ActivatableObject o)
                {
                    o.Activate();
                }

                screen.ScreenEffects.Add(new CameraShakeEffect(screen, 5, 20));
            }

            GameInput.AddInputToString(ref TextTest);

            // Update component last
            base.Update(gameTime);
        }

        // Draw player
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //string hihi = "DOP KEK MEMEM\nHEHE NICE MEME MEME BOBO";
            //float memeX = GameFonts.GahameFont.MeasureString(hihi).X/2;
            //GameFonts.GahameFont.DrawString(spriteBatch, hihi, new Vector2(Position.X-memeX, Position.Y - 64), Color.Black);

            Vector2 origin = GameFonts.Arial.MeasureString(TextTest) / 2;
            //GameFonts.GahameFont.DrawString(spriteBatch, GameFont.Gahamefy(TextTest), Position - origin - new Vector2(0, 48), Color.Blue);
            spriteBatch.DrawString
            (
                GameFonts.Arial,
                TextTest,
                Position - new Vector2(0, 48),
                Color.DarkBlue,
                0,
                origin,
                1,
                SpriteEffects.None,
                0f
            );

            /*spriteBatch.DrawString(
                    GameFonts.Arial,
                    TextTest,
                    Position + new Vector2(0, -48),
                    Color.Pink,
                    0,
                    origin,
                    1,
                    SpriteEffects.None,
                    0
                    );*/

            // Speed test
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.X.ToString()).X / 2, 32), Color.Black);
            spriteBatch.DrawString(GameFonts.Arial, physics.Velocity.Y.ToString(), Position - new Vector2(GameFonts.Arial.MeasureString(physics.Velocity.Y.ToString()).X / 2, 42), Color.Black);
        }

        #region Movement

        // Walk horizontally
        public override void WalkHorizontal(float speed)
        {
            // Approach the speed
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, speed, accelerationSpeed * GahameController.GameSpeed);
            WalkingHorizontal = true;
        }

        // Walk vertically
        public override void WalkVertical(float speed)
        {
            // Approach the speed
            physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, speed, accelerationSpeed * GahameController.GameSpeed);
            WalkingVertical = true;
        }

        // Stop horizontal speed
        public override void StopHorizontal()
        {
            // Approach 0 on horizontal speed
            physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, slowDownSpeed * GahameController.GameSpeed);
        }

        // Stop horizontal speed
        public override void StopVertical()
        {
            // Approach 0 on Vertical speed
            physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, 0, slowDownSpeed * GahameController.GameSpeed);
        }

        #endregion

    }
}

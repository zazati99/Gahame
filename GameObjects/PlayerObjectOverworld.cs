using Gahame.GameUtils;
using Gahame.GameScreens;
using Gahame.GameObjects.ObjectComponents;
using Gahame.GameObjects.ObjectComponents.Colliders;
using Gahame.GameObjects.ObjectComponents.DialogueSystem;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gahame.GameObjects
{

    // Overworld version of player object
    public class PlayerObjectOverworld : GameObject
    {
        // Componenst
        Sprite sprite;
        HitBox hitBox;
        Physics physics;

        // Controlls variablees
        float maxSpeed;
        float accelerationSpeed;
        float slowDownSpeed;

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
            screen.CamController.target = this;
            screen.CamController.CamOffset.Y = -16;
            screen.CamController.MoveAmount = new Vector2(.2f, .2f);
            //screen.CamController.Static = true;
            //screen.CamController.SetPosition(new Vector2(330, 100));

            // Controlls variables
            maxSpeed = 2;
            accelerationSpeed = .5f;
            slowDownSpeed = .25f;

        }

        // Update player
        public override void Update(GameTime gameTime)
        {

            // Keyboard controlls
            if (!GameControlls.ControllerMode){
                // Walking left and right
                if (GameControlls.RightCD || GameControlls.LeftCD)
                {
                    sprite.SpriteScale.X = MyMaths.Lerp(sprite.SpriteScale.X, (GameControlls.RightCD ? 1 : 0) - (GameControlls.LeftCD ? 1 : 0), .25f * GahameController.GameSpeed);
                    // Approach max X velocity
                    physics.Velocity.X = MyMaths.Approach(physics.Velocity.X,
                        maxSpeed * ((GameControlls.RightCD ? 1 : 0) - (GameControlls.LeftCD ? 1 : 0)),
                        GahameController.GameSpeed * accelerationSpeed);
                }
                else physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, GahameController.GameSpeed * slowDownSpeed);

                // Walking up and down
                if (GameControlls.UpCD || GameControlls.DownCD)
                {
                    // Approach max Y velocity
                    physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y,
                        maxSpeed * ((GameControlls.DownCD ? 1 : 0) - (GameControlls.UpCD ? 1 : 0)),
                        GahameController.GameSpeed * accelerationSpeed);
                }
                else physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, 0, GahameController.GameSpeed * slowDownSpeed);
            } else // Gamepad Controlls
            {

                // Approach max xspeed
                if (GameControlls.RightCD || GameControlls.LeftCD)
                {
                    // Sprite scale (prob wont keep this)
                    sprite.SpriteScale.X = MyMaths.Lerp(sprite.SpriteScale.X, (GameControlls.RightCD ? 1 : 0) - (GameControlls.LeftCD ? 1 : 0), .25f * GahameController.GameSpeed);

                    // approach max xspeed
                    physics.Velocity.X = MyMaths.Approach(physics.Velocity.X,
                                                          GameControlls.LeftStickX * maxSpeed,
                                                          GahameController.GameSpeed * accelerationSpeed);

                } 
                else physics.Velocity.X = MyMaths.Approach(physics.Velocity.X, 0, slowDownSpeed * GahameController.GameSpeed);    

                // Approach max yspeed
                if (GameControlls.UpCD || GameControlls.DownCD)
                {
                    // approach max yspeed
                    physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y,
                                                          GameControlls.LeftStickY * maxSpeed,
                                                          GahameController.GameSpeed * accelerationSpeed);
                } 
                else physics.Velocity.Y = MyMaths.Approach(physics.Velocity.Y, 0, slowDownSpeed * GahameController.GameSpeed);  
                
            }

            // Update component last
            base.Update(gameTime);
        }

        // Draw player
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

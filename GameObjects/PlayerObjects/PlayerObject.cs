using Gahame.GameScreens;
using Microsoft.Xna.Framework;

namespace Gahame.GameObjects
{
    public class PlayerObject : GameObject
    {
        // Nice things
        public bool WalkingHorizontal;
        public bool WalkingVertical;

        // Constructor
        public PlayerObject(GameScreen screen) : base(screen)
        {
            // Make this the screens player object
            screen.Player = this;
        }

        // DO this at start before anything else in update
        protected void StartUpdate()
        {
            WalkingHorizontal = false;
            WalkingVertical = false;
        }

        // Update that will be overridden
        public override void Update(GameTime gameTime)
        {
            // Stop if not walking
            if (!WalkingHorizontal) StopHorizontal();
            if (!WalkingVertical) StopVertical();

            // DO update for gameObject
            base.Update(gameTime);
        }

        #region Movement

        // Walking horizontally need to do base thing
        public virtual void WalkHorizontal(float speed)
        {

        }

        // Walking Vertically need base
        public virtual void WalkVertical(float speed)
        {

        }

        // Stopping horizontal speed
        public virtual void StopHorizontal()
        {

        }

        // Stop le vertical speed
        public virtual void StopVertical()
        {

        }

        #endregion

    }
}

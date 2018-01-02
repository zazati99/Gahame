using Gahame.GameScreens;

namespace Gahame.GameObjects
{
    public class ActivatableObject : GameObject
    {
        // Check is this boy is accesible
        public bool Accessible;

        // Constructor
        public ActivatableObject(GameScreen screen) : base(screen)
        {

        }

        // Constructor without screen stuff
        public ActivatableObject() : base()
        {

        }

        // Activate function that will be called
        public void Activate()
        {
            if (Accessible) ActivateDefenition();
        }

        // activate logic here
        protected virtual void ActivateDefenition()
        {

        }
    }
}

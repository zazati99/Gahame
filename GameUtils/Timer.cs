namespace Gahame.GameUtils
{

    // Timer class (should stop at -1 and activate at 0)
    public class Timer
    {
        // time variable
        float timeInFrames;

        // Should timer ignore gamespeed?
        public bool IgnoreGameSpeed;

        // constructor without setting timer
        public Timer()
        {
            timeInFrames = -1;
            IgnoreGameSpeed = false;
        }

        // constructor where we set the timer
        public Timer(int timeInFrames)
        {
            this.timeInFrames = timeInFrames;
            IgnoreGameSpeed = false;
        }

        // constructor where we set the timer an lets you choose to ignore game speed
        public Timer(int timeInFrames, bool ignoreGameSpeed)
        {
            this.timeInFrames = timeInFrames;
            IgnoreGameSpeed = ignoreGameSpeed;
        }

        // Check and tick the timer
        public bool Check()
        {
            return timeInFrames <= 0;
        }
        public void Tick()
        {
            timeInFrames -= (IgnoreGameSpeed ? 1 : GahameController.GameSpeed);
        }
        public bool CheckAndTick()
        {
            Tick();
            return Check();
        }

        // Set timer functions
        // Set timer in frames
        public void SetFrames(int frames)
        {
            timeInFrames = frames;
        }

        // Set timer in seconds
        public void SetSeconds(float seconds)
        {
            timeInFrames = (60 * seconds);
        }

    }
}

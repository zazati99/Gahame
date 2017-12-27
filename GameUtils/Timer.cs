namespace Gahame.GameUtils
{

    // Timer class (should stop at -1 and activate at 0)
    public class Timer
    {
        // time variable
        int time;

        // constructor without setting timer
        public Timer()
        {
            time = -1;
        }

        // constructor where we set the timer
        public Timer(int time)
        {
            this.time = time;
        }

        // Updated timer and checks if it's 0
        public bool Check()
        {
            time--;
            return (time == 0);
        }

        // Set timer functions
        // Set timer in frames
        public void SetFrames(int frames)
        {
            time = frames;
        }

        // Set timer in seconds
        public void SetSeconds(float seconds)
        {
            time = (int)(60 * seconds);
        }

    }
}

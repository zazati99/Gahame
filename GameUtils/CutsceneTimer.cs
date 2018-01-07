namespace Gahame.GameUtils
{
    public class CutsceneTimer
    {
        // the time
        int time;

        // Is timer start ?!?
        bool timerStarted;

        // Constructor
        public CutsceneTimer()
        {
            time = 0;
            timerStarted = false;
        }

        // Timer update
        public void StartTimer()
        {
            timerStarted = true;
        }

        // Update timer
        public void Update()
        {
            if (timerStarted) time++;
        }

        // DO something at specific time
        public bool EventAtTime(int timeInFrames)
        {
            return time == timeInFrames;
        }

        // Do something between these frames
        public bool EventDuring(int firstFrame, int secondFrame)
        {
            return time >= firstFrame && time <= secondFrame;
        }
    }
}

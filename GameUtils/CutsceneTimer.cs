namespace Gahame.GameUtils
{
    public class CutsceneTimer
    {
        // the time
        int time;

        // Is timer start ?!?
        bool timerUpdate;

        // Constructor
        public CutsceneTimer()
        {
            time = 0;
            timerUpdate = false;
        }

        // Timer update
        public void Start()
        {
            timerUpdate = true;
        }

        // Pause the timer
        public void Pause()
        {
            timerUpdate = false;
        }

        // Stop
        public void Stop(){
            timerUpdate = false;
            time = 0;
        }

        // Update timer
        public void Update()
        {
            if (timerUpdate) time++;
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

using System.Collections.Generic;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Utils
{
    public static class TimerController
    {
        public static void Add(string id, float time)
        {
            m_timersToAdd.Add(new Timer(id, time));
        }

        public static void GameUpdate(float delta)
        {
            while(m_timersToAdd.Count > 0)
            {
                m_timers.Add(m_timersToAdd[0]);
                m_timersToAdd.RemoveAt(0);
            }

            foreach (Timer timer in m_timers)
            {
                timer.UpdateTime(delta);
                if(timer.time <= 0.0f)
                {
                    Game.SetOffTrigger(timer.id + "_Over");
                    m_timers.Remove(timer);
                }

                if (m_timers.Count <= 0) 
                    break;
            }
        }

        private static List<Timer> m_timersToAdd = new List<Timer>();
        private static List<Timer> m_timers = new List<Timer>();
    }

    public class Timer
    {
        public string id { get; private set; }
        public float time { get; private set; }

        public Timer(string id, float time)
        {
            this.id = id;
            this.time = time;
        }

        public void UpdateTime(float delta)
        {
            time -= delta;
        }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace RainbowCat.TheSapling.InternalStructure
{
    [System.Serializable]
    public class Conditional
    {
        public const string onStart = "OnStart";
        public const string onEnd = "OnEnd";
        public const string onTimerOver = "_Over";

        public enum TriggerType
        {
            OnStart,
            OnEnd,
            OnEvent,
            OnTimerOver,
        }

        public string triggerID 
        { 
            get
            {
                if (m_triggerType == TriggerType.OnStart)
                    return onStart;
                else if (m_triggerType == TriggerType.OnEnd)
                    return onEnd;
                else if (m_triggerType == TriggerType.OnTimerOver)
                    return triggerID + "_Over";
                else
                    return triggerID;
            }
        }

        protected string m_triggerID;
        protected TriggerType m_triggerType;
        protected List<Action> m_actions;
    }
}


namespace RainbowCat.TheSapling.InternalStructure
{
    [System.Serializable]
    public struct SequenceStep
    {
        public const string onStart = "OnStart";
        public const string onEnd = "onEnd";

        public enum TriggerType
        {
            OnStart,
            OnInteractionStart,
            OnInteractionEnd,
            OnMinigameCompleted,
            OnTimerOver,
            OnEnd
        }

        public enum ActionType
        {
            ActivateInteractable,
            ActivateHint,
            ChangeCamera,
            StartCinematic,
            StartMinigame,
            EndMinigame,
            PaintCinematicObject,
            StartTimer,
            TriggerTransition,
        }

        public string triggerID;
        public TriggerType trigger;
        public ActionType action;
        public bool undoAction;
        public string code;
        public float delay;

        public string trueTrigger
        {
            get
            {
                if (trigger == TriggerType.OnStart)
                    return onStart;
                else if (trigger == TriggerType.OnEnd)
                    return onEnd;
                else if (trigger == TriggerType.OnTimerOver)
                    return triggerID + "_Over";
                else
                    return triggerID;
            }
        }
    }
}
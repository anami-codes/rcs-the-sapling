namespace RainbowCat.TheSapling.Transitions
{
    public class Transition
    {
        public enum Fase
        {
            None,
            Out,
            Out_Wait,
            In,
            In_Wait
        }

        public bool ready { get; protected set; }
        public Fase fase { get; protected set; }

        public virtual void Out(float transitionTime)
        {
            ready = false;
            fase = Fase.Out;
            this.transitionTime = transitionTime;
        }

        public virtual void In(float transitionTime) 
        {
            ready = false;
            fase = Fase.In;
            this.transitionTime = transitionTime;
        }

        public virtual void GameUpdate(float delta) { }

        public void SetToWaiting()
        {
            fase = (fase == Fase.Out) ? Fase.Out_Wait : Fase.In_Wait;
        }

        public virtual void Clear()
        {
            fase = Fase.None;
        }

        protected float transitionTime;
    }

    [System.Serializable]
    public struct TransitionInfo
    {
        public enum Type
        {
            None,
            Fade,
            Movement
        }

        public enum Target
        {
            None,
            StartPage,
            FinishPage,
            StartChapter,
            FinishChapter
        }

        public string triggerID;
        public Target target;
        public Type type;
        public string code;
    }
}
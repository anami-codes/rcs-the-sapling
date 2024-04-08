using RainbowCat.TheSapling.Transitions;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class Entity
    {
        public virtual void BeginTransitionEvent(string code, string message)
        {
            if(callbackBegin != null)
            {
                callbackBegin(code, message);
                callbackBegin = null;
            }
        }

        public virtual void MidTransitionEvent(string code, string message)
        {
            if(callbackMiddle != null)
            {
                callbackMiddle(code, message);
                callbackMiddle = null;
            }
        }

        public virtual void EndTransitionEvent(string code, string message)
        {
            if(callbackEnd != null)
            {
                callbackEnd(code, message);
                callbackEnd = null;
            }
        }

        public delegate void Callback(string code, string message);
        protected Callback callbackBegin;
        protected Callback callbackMiddle;
        protected Callback callbackEnd;
    }
}
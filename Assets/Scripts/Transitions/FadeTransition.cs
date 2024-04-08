using RainbowCat.TheSapling.TechArt;

namespace RainbowCat.TheSapling.Transitions
{
    public class FadeTransition : Transition
    {

        public override void Out(float transitionTime)
        {
            base.Out(10.0f);
            t = 0.0f;
            isOut = true;
        }

        public override void In(float transitionTime)
        {
            base.In(10.0f);
            t = 0.0f;
            isOut = false;
        }

        public override void GameUpdate(float delta)
        {
            if (!ready)
            {
                t += transitionTime * delta;
                float fadePercent = t / transitionTime;
                WatercolorShader.Paint((isOut) ? 1.0f - fadePercent : fadePercent);
                if(fadePercent >= 1.0f)
                {
                    ready = true;
                }
            }
        }

        protected bool isOut;
        protected float t;

    }
}
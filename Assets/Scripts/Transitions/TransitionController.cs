using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Transitions
{
    public static class TransitionController
    {
        public static bool isInTransition { get; private set; }
        public static bool canChangeUI 
        {
            get
            { return 
                    (m_transition?.fase == Transition.Fase.In_Wait) || 
                    (m_transition?.fase == Transition.Fase.Out); }
        }

        public static void Receiver(TransitionInfo info, Entity sender)
        {
            if (!isInTransition)
            {
                isInTransition = true;
                Game.manager.ChangeState(Game.State.IN_TRANSITION);
                m_sender = sender;
                SetTransition(info);
            }
        }

        public static void GameUpdate(float delta)
        {
            if (isInTransition)
            {
                if (!m_transition.ready)
                    m_transition.GameUpdate(delta);
                else
                {
                    if (m_transition.fase == Transition.Fase.Out)
                    {
                        m_transition.SetToWaiting();
                        m_sender.MidTransitionEvent(m_info.code, m_info.triggerID);
                        m_timer = m_baseTransitionTime;
                    }
                    else if (m_transition.fase == Transition.Fase.In)
                    {
                        m_transition.SetToWaiting();
                        m_sender.EndTransitionEvent(m_info.code, m_info.triggerID);
                        m_timer = 0.1f;
                    }
                }

                if (m_timer > 0.0f)
                {
                    m_timer -= delta;
                    if(m_timer <= 0.0f)
                    {
                        if (m_transition.fase == Transition.Fase.Out_Wait)
                            m_transition.In(m_baseTransitionTime);
                        else if (m_transition.fase == Transition.Fase.In_Wait)
                            Clear();
                    }
                }
            }
        }

        private static void SetTransition(TransitionInfo info)
        {
            switch (info.type)
            {
                case TransitionInfo.Type.Fade:
                    m_transition = new FadeTransition();
                    break;
            }

            m_transition.Out(m_baseTransitionTime);
            m_info = info;
            isInTransition = true;
            m_sender.BeginTransitionEvent(info.code, info.triggerID);
        }

        private static void Clear()
        {
            m_transition.Clear();
            m_transition = null;
            m_sender = null;
            m_info = new TransitionInfo();

            m_timer = 0.0f;

            isInTransition = false;
            Game.manager.ChangeState(Game.State.WAITING_FOR_INPUT);
        }

        private static TransitionInfo m_info;
        private static Transition m_transition;
        private static Entity m_sender;

        private static float m_timer;
        private static float m_baseTransitionTime = 1.0f;
    }
}
using System.Collections.Generic;
using RainbowCat.TheSapling.Utils;

namespace RainbowCat.TheSapling.InternalStructure
{
    using StepAction = SequenceStep.ActionType;

    public class CinematicManager
    {
        public CinematicManager (Page page)
        {
            this.page = page;
            m_cinematicObjs = page.manager.GetComponentsInChildren<CinematicObject>();

            ActivateCinematicObjects(false);
        }

        public void PlaySequence(Queue<SequenceStep> sequence)
        {
            if (sequence.Count <= 0) return;
            m_delayedSteps = new List<SequenceStep>();
            while (sequence.Count > 0)
            {
                SequenceStep step = sequence.Dequeue();

                if (step.delay <= 0.0f || step.action == StepAction.StartTimer)
                    DoStep(step);
                else
                    m_delayedSteps.Add(step);
            }
        }

        private CinematicObject GetCinematicObject(string objName)
        {
            for (int i = 0; i < m_cinematicObjs.Length; i++)
            {
                if (m_cinematicObjs[i].id == objName)
                    return m_cinematicObjs[i];
            }

            UnityEngine.Debug.LogError("Couldn't find CinematicObject " + objName);
            return null;
        }

        private void ActivateCinematicObjects(bool activate)
        {
            foreach (CinematicObject obj in m_cinematicObjs)
            {
                obj.gameObject.SetActive(activate);
            }
        }

        public void GameUpdate(float delta)
        {
            foreach (CinematicObject obj in m_cinematicObjs)
            {
                obj.GameUpdate(delta);
            }

            if (m_delayedSteps != null && m_delayedSteps.Count > 0)
            {
                m_delayedTimer += delta;
                CheckDelayedSteps();
                if (m_delayedSteps.Count <= 0)
                    m_delayedTimer = 0.0f;
            }
        }

        protected void DoStep(SequenceStep step)
        {
            switch (step.action)
            {
                case StepAction.ActivateInteractable:
                    page.interactionManager.GetInteractable(step.code).Activate(!step.undoAction);
                    if (!step.undoAction) Game.manager.ChangeState(Game.State.WAITING_FOR_INPUT);
                    return;
                case StepAction.PaintCinematicObject:
                    GetCinematicObject(step.code).Paint(step.undoAction);
                    break;
                case StepAction.ActivateHint:
                    page.interactionManager.ActivateHint(step.code, true);
                    if (!step.undoAction) Game.manager.ChangeState(Game.State.WAITING_FOR_INPUT);
                    return;
                case StepAction.ChangeCamera:
                    Game.cameraController.ChangeCamera(step.code);
                    break;
                case StepAction.StartCinematic:
                    page.manager.PlayAnimation(step.code);
                    break;
                case StepAction.StartMinigame:
                    page.interactionManager.ActivateAll(true);
                    page.minigame.ActivateTargets(true);
                    Game.manager.ChangeState(Game.State.WAITING_FOR_INPUT);
                    return;
                case StepAction.EndMinigame:
                    page.interactionManager.Clear();
                    page.manager.PlayAnimation("Result");
                    break;
                case StepAction.StartTimer:
                    TimerController.Add(step.code, step.delay);
                    break;
                case StepAction.TriggerTransition:
                    Game.SetOffTrigger(step.code);
                    break;
            }

            Game.manager.ChangeState(Game.State.PLAYING_CINEMATIC);
        }

        private void CheckDelayedSteps()
        {
            for (int i = 0; i < m_delayedSteps.Count; i++)
            {
                if (m_delayedSteps[i].delay <= m_delayedTimer)
                {
                    DoStep(m_delayedSteps[i]);
                    m_delayedSteps.Remove(m_delayedSteps[i]);
                }
            }
        }

        private Page page;
        private CinematicObject[] m_cinematicObjs;

        private List<SequenceStep> m_delayedSteps;
        private float m_delayedTimer;
    }
}
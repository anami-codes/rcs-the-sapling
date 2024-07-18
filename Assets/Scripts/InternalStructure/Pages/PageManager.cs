using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using RainbowCat.TheSapling.Transitions;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class PageManager : MonoBehaviour
    {
        [Header("Page Info")]
        public string id;
        public RectTransform ui;
        public CinemachineVirtualCamera startingCam;
        public SpriteRenderer[] backgrounds;
        public bool isMinigame { get { return page.isMinigame; } }

        [Header("")]
        public TransitionInfo[] transitions;
        public SequenceStep[] steps;

        //Initializing manager and setting up justStarted bool for
        //checking and cleaning during next frame.
        private void Awake()
        {
            ChapterManager chapter = FindAnyObjectByType<ChapterManager>();
            Initialize(chapter);

            m_anim = GetComponent<Animator>();
            m_justStarted = true;
        }

        protected virtual void Initialize(ChapterManager chapter)
        {
            page = new Page(id, chapter, this);
        }

        // GameUpdate function runs when game isn't paused, in transition or loading a new scene.
        public void GameUpdate(float delta)
        {
            page.GameUpdate(delta);
        }

        //This Monobehaviour Update is for non game dependant functionality.
        //i.e: The UI waiting to turn on/off happend *during* transitions, so it must go here.
        private void Update()
        {
            if (m_justStarted)
            {
                if (page.chapter.pages[page.chapter.firstPageIndex].id != page.id)
                    Clear();
                else
                    BeginPage();

                m_justStarted = false;
            }

            if (uiIsWaiting && TransitionController.canChangeUI)
            {
                uiIsWaiting = false;
                ui?.gameObject.SetActive(isActive);
            }
        }

        public TransitionInfo SetOffTrigger(string triggerID)
        {
            if( HasTransition(triggerID) )
            {
                TransitionInfo info = GetTransition(triggerID);
                if ((info.target == TransitionInfo.Target.FinishPage) ||
                    (info.target == TransitionInfo.Target.FinishChapter))
                {
                    isActive = false;
                    uiIsWaiting = true;
                }
                return (page.SetOffTrigger(info)) ? new TransitionInfo() : info;
            }
            else if (HasStep(triggerID))
            {
                Advance(triggerID);
            }

            return new TransitionInfo();
        }

        public virtual void BeginPage()
        {
            isActive = true;
            gameObject.SetActive(true);
            uiIsWaiting = true;
            page.chapter.UpdateCurrentPage(page.id);
            SoundManager.instance.PlayMusic( (page.isMinigame) ? 1 : 0);
            if(startingCam != null)
            {
                Game.cameraController.ChangeCamera(startingCam.name);
            }
            else
            {
                Game.cameraController.ChangeCamera(CameraController.defaultCamera);
                Game.UpdateCenter(transform.position.x, transform.position.y);
            }
            Advance(SequenceStep.TriggerType.OnStart);
        }

        public void EndPage(string code, string message)
        {
            isActive = false;
            uiIsWaiting = true;
            gameObject.SetActive(false);
            page.chapter.GetPage(code).BeginPage();
        }

        public void PlayAnimation(string animName)
        {
            m_anim.Play(animName);
        }

        private void Advance(string triggerID)
        {
            if (triggerID != SequenceStep.onStart && triggerID != SequenceStep.onEnd)
            {
                page.Advance(GetSequence(triggerID));
            }
        }

        private void Advance(SequenceStep.TriggerType triggerType)
        {
            if (triggerType == SequenceStep.TriggerType.OnStart)
            {
                page.Advance(GetSequence(SequenceStep.onStart));
            }
            else if (triggerType == SequenceStep.TriggerType.OnEnd)
            {
                page.Advance(GetSequence(SequenceStep.onEnd));
            }
        }

        private bool HasTransition(string triggerID)
        {
            for(int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].triggerID == triggerID) return true;
            }
            return false;
        }

        private bool HasStep(string triggerID)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                if (steps[i].trueTrigger == triggerID) return true;
            }
            return false;
        }

        private TransitionInfo GetTransition(string triggerID)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].triggerID == triggerID)
                    return (transitions[i]);
            }
            return new TransitionInfo();
        }

        private Queue<SequenceStep> GetSequence(string triggerID)
        {
            Queue<SequenceStep> nextSteps = new Queue<SequenceStep>();
            foreach (SequenceStep step in steps)
            {
                if (step.trueTrigger == triggerID)
                    nextSteps.Enqueue(step);
            }

            return nextSteps;
        }

        protected virtual void Clear()
        {
            gameObject.SetActive(false);
            ui?.gameObject.SetActive(false);
            isActive = false;
        }

        protected Page page;
        protected Animator m_anim;

        private bool m_justStarted;
        private float onStartTimer;
        private bool uiIsWaiting = false;
        private bool isActive = true;
    }
}
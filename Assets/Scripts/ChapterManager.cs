using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RainbowCat.TheSapling
{
    public class ChapterManager : MonoBehaviour
    {
        void Awake()
        {
            PageInfo[] pages_ = FindObjectsOfType<PageInfo>();
            foreach (PageInfo page in pages_)
            {
                if(!pages.ContainsKey(page.id) && page.id != "")
                    pages.Add(page.id, page);
            }
        }

        private void Start()
        {
            GameManager.instance.SetChapter(this);
        }

        public virtual void AdvanceSequence()
        {

        }

        public void ChangeScene(int nextSceneID, Animator transition, float waitTime = 1.0f)
        {
            this.nextSceneID = nextSceneID;
            this.transition = transition;
            timer = waitTime;
            changeScene = true;
        }

        public void ChangeTo(string nextPage, string animState, Callback callback, float transitionTime = 1.0f)
        {
            if(currentPage.id != nextPage)
            {
                if(currentPage.isMinigame)
                {
                    currentPage.ActivateInteractables(false);
                    currentPage.minigameManager.ActivateTargets(false);
                }

                currentPage = pages[nextPage];
                ChangeCamera(currentPage.id);
            }

            if(currentPage.isMinigame)
            {
                GameManager.instance.ChangeState(GameManager.GameStatus.MINIGAME_START);
                currentPage.ActivateInteractables(true);
                currentPage.minigameManager.ActivateTargets(true);
            }
            else
            {
                GameManager.instance.ChangeState(GameManager.GameStatus.PLAYING_CINEMATIC);
            }
            
            currentPage.anim.Play(animState);
            ActionAfterTransition(callback, currentPage.anim, transitionTime);
        }

        public virtual void GameUpdate(float delta)
        {
            if (changeScene)
            {
                if(CheckAnimation(delta))
                {
                    changeScene = false;
                    SceneManager.LoadScene(nextSceneID, LoadSceneMode.Single);
                }
            }

            if (waitingCallback)
            {
                if (CheckAnimation(delta))
                {
                    waitingCallback = false;
                    callback();
                }
            }

            if (currentPage != null) currentPage.GameUpdate(delta);
        }

        protected bool CheckAnimation(float delta)
        {
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                timer -= delta;
                return (timer <= 0.0f);
            }
            return false;
        }

        protected void ActionAfterTransition(Callback callback, Animator transition, float waitTime = 1.0f)
        {
            this.callback = callback;
            this.transition = transition;
            timer = waitTime;
            waitingCallback = true;
        }

        protected void ChangeCamera(string pageInfo)
        {
            foreach(KeyValuePair<string, PageInfo> page in pages)
            {
                Camera cam_ = page.Value.cam;
                if(page.Value.id == pageInfo)
                {
                    cam_.tag = "MainCamera";
                    cam_.targetDisplay = 0;
                    cam_.gameObject.SetActive(true);
                } 
                else
                {
                    cam_.tag = "Untagged";
                    cam_.targetDisplay = 1;
                    cam_.gameObject.SetActive(false);
                }
            }
            GameManager.instance.ChangeCamera(pages[pageInfo].cam);
        }

        protected void ChangeHintState(string hintName, bool activate)
        {
            InteractableObject interactable = currentPage.GetInteractable(hintName);
            interactable.gameObject.SetActive(activate);
            if (activate)
            {
                interactable.hint.StartHint();
                GameManager.instance.ChangeState(GameManager.GameStatus.WAITING_FOR_INPUT);
            }
            else
            {
                interactable.hint.StopHint();
            }

        }

        protected void StartMinigame()
        {
            GameManager.instance.ChangeState(GameManager.GameStatus.IN_MINIGAME);
            //Show Hint
        }

        public virtual void EndMinigame()
        {

        }

        private bool changeScene;
        private int nextSceneID;
        private float timer;
        private Animator transition;

        protected Dictionary<string, PageInfo> pages = new Dictionary<string, PageInfo>();
        protected PageInfo currentPage;

        public delegate void Callback();
        protected Callback callback;
        private bool waitingCallback;
    }
}
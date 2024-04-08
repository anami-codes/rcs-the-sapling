using UnityEngine;
using Cinemachine;
using RainbowCat.TheSapling.Transitions;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class ChapterManager : MonoBehaviour
    {
        public string id;
        public int firstPageIndex;
        public PageManager[] pages;
        public CinemachineBrain cam { get; protected set; }
        public PageManager currentPage { get { return chapter.currentPage; } }

        private void Awake()
        {
            chapter = new Chapter(id, pages, this);
            cam = GetComponentInChildren<CinemachineBrain>();
        }

        public void SetOffTrigger(string triggerID)
        {
            TransitionInfo info = chapter.currentPage.SetOffTrigger(triggerID);
            if (info.triggerID != "" && info.triggerID != null)
                chapter.SetOffTrigger(info);
        }

        public void UpdateCurrentPage(string newCurrent)
        {
            chapter.UpdateCurrentPage(newCurrent);
        }

        public void GameUpdate(float delta)
        {
            chapter.currentPage.GameUpdate(delta);
        }

        public PageManager GetPage(string pageID)
        {
            return chapter.GetPage(pageID);
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

        protected void StartMinigame()
        {
            Game.manager.ChangeState(Game.State.IN_MINIGAME);
            //Show Hint
        }

        public virtual void EndMinigame()
        {

        }

        protected Chapter chapter;

        private float timer;
        private Animator transition;
    }
}
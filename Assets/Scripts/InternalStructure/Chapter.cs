using System.Collections.Generic;
using RainbowCat.TheSapling.Transitions;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class Chapter : Entity
    {
        public string id { get; protected set; }
        public ChapterManager manager { get; protected set; }
        public PageManager currentPage {
            get { return pages[m_currentPage]; }
        }

        public Chapter(string id, PageManager[] pages_, ChapterManager manager)
        {
            this.id = id;
            this.manager = manager;
            pages = new Dictionary<string, PageManager>();
            for(int i = 0; i < pages_.Length; i++){
                if(!pages.ContainsKey(pages_[i].id))
                {
                    pages.Add(pages_[i].id, pages_[i]);
                    if (i == manager.firstPageIndex)
                        m_currentPage = pages_[i].id;
                }
            }
        }

        public void UpdateCurrentPage(string newCurrent)
        {
            m_currentPage = newCurrent;
        }

        public virtual void GameUpdate(float delta)
        {
            if (currentPage != null)
                currentPage.GameUpdate(delta);
        }

        public bool SetOffTrigger(TransitionInfo info)
        {
            switch (info.target)
            {
                case TransitionInfo.Target.FinishChapter:
                    return Finish(info);
            }
            return false;
        }

        public PageManager GetPage(string pageID)
        {
            if (pages.ContainsKey(pageID))
                return pages[pageID];
            return null;
        }

        private bool Finish(TransitionInfo info)
        {
            callbackBegin = Game.manager.ShowLoadingPanel;
            callbackMiddle = Game.manager.ChangeScene;
            callbackEnd = Game.manager.ShowLoadingPanel;
            return Game.manager.CallTransition(info, this);
        }

        protected Dictionary<string, PageManager> pages;
        protected string m_currentPage;
    }
}
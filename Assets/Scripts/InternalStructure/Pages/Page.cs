using System.Collections.Generic;
using RainbowCat.TheSapling.Transitions;

using RainbowCat.TheSapling.Utils;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class Page : Entity
    {
        public string id { get; protected set; }
        public bool isMinigame { get; protected set; }
        public Minigame minigame { get; protected set; }
        public ChapterManager chapter { get; protected set; }
        public PageManager manager { get; protected set; }

        public Page(string id, ChapterManager chapter, PageManager pageManager, Minigame.Type minigameType = Minigame.Type.None)
        {
            this.id = id;
            this.chapter = chapter;
            manager = pageManager;

            isMinigame = (minigameType != Minigame.Type.None);
            if (isMinigame)
                minigame = new Minigame(this, minigameType);

            interactionManager = new InteractionManager(this);
            cinematicManager = new CinematicManager(this);
        }

        public void Advance(Queue<SequenceStep> sequence)
        {
            if (sequence.Count <= 0) return;
            cinematicManager.PlaySequence(sequence);
        }

        public bool SetOffTrigger(TransitionInfo info)
        {
            switch (info.target)
            {
                case TransitionInfo.Target.FinishPage:
                    return Finish(info);
            }
            return false;
        }

        public void GameUpdate(float delta)
        {
            interactionManager.GameUpdate(delta);
            cinematicManager.GameUpdate(delta);

            if (isMinigame) 
                minigame.GameUpdate(delta);
        }

        private bool Finish(TransitionInfo info)
        {
            Clear();
            callbackMiddle = manager.EndPage;
            return Game.manager.CallTransition(info, this);
        }

        private void Clear()
        {
            interactionManager.Clear();
            if (isMinigame)
                chapter.EndMinigame();
        }

        public CinematicManager cinematicManager { get; private set; }
        public InteractionManager interactionManager { get; private set; }

    }
}
using System.Collections;
using System.Collections.Generic;
using RainbowCat.TheSapling.Minigames;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class Minigame
    {
        public enum Type
        {
            None,
            DragAndWait,
            DragAndTouch,
            Collecting,
            Match
        }
        public Type type { get; protected set; }

        public Minigame(Page page, Type minigameType)
        {
            this.page = page;
            type = minigameType;
            MinigameManager manager = page.manager as MinigameManager;
            for(int i = 0; i < manager.targets.Length; i++)
            {
                AddTarget(manager.targets[i]);
            }
        }

        public void AddTarget(MinigameTarget minigameTarget)
        {
            targets.Add(minigameTarget);
            minigameTarget.Initialize(this);
            minigameTarget.gameObject.SetActive(false);
        }

        public void ActivateTargets(bool activate)
        {
            foreach (MinigameTarget target in targets)
            {
                target.gameObject.SetActive(activate);
            }
        }

        public void CheckTargets()
        {
            foreach (MinigameTarget target in targets)
            {
                if (target.isNecessary && !target.isReady)
                    return;
            }
            Game.SetOffTrigger("EndMinigame");
        }

        public void GameUpdate(float delta)
        {
            foreach (MinigameTarget obj in targets)
            {
                obj.GameUpdate(delta);
            }
        }

        protected Page page;
        protected List<MinigameTarget> targets = new List<MinigameTarget>();
    }
}
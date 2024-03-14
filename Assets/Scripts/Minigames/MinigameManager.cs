using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        public void SetPage(PageInfo page)
        {
            this.page = page;
        }

        public void AddTarget(MinigameTarget minigameTarget)
        {
            minigameTargets.Add(minigameTarget);
            minigameTarget.gameObject.SetActive(false);
        }

        public void ActivateTargets(bool activate)
        {
            foreach (MinigameTarget target in minigameTargets)
            {
                target.gameObject.SetActive(activate);
            }
        }

        public void CheckTargets()
        {
            foreach (MinigameTarget target in minigameTargets)
            {
                if (target.isNecessary && !target.isReady)
                    return;
            }
            page.Finish();
        }

        public void GameUpdate(float delta)
        {
            foreach(MinigameTarget obj in minigameTargets)
            {
                obj.GameUpdate(delta);
            }
        }

        protected PageInfo page;
        protected List<MinigameTarget> minigameTargets = new List<MinigameTarget>();
    }
}
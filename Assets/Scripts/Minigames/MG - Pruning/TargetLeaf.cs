using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.Pruning
{
    public class TargetLeaf : MinigameTarget
    {
        public void Cut()
        {
            if (!isReady)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                // Animate fall
                SetAsReady();
            }
        }
    }
}
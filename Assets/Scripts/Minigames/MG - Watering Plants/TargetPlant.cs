using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.WateringPlants
{
    public class TargetPlant : MinigameTarget
    {
        public void Water(float delta)
        {
            if (!isReady)
            {
                timer += delta;
                if (timer >= 2.0f)
                {
                    timer = 0.0f;
                    anim.SetTrigger("ChangeStage");
                    stage++;

                    if (stage == 3) SetAsReady();
                }
            }
        }
    }
}
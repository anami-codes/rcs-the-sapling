using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMinigame : BaseMinigame
{
    public RectTransform stoneContainer;

    public override void StartMinigame(GameManager gameManager)
    {
        if ( !this.gameManager )
        {
            base.StartMinigame(gameManager);
            stones = stoneContainer.GetComponentsInChildren<Stone>();
        }

        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].Restart(this);
        }
    }

    public override void CheckConditions()
    {
        base.CheckConditions();
        for (int i = 0; i < stones.Length; i++)
        {
            if (stones[i].life > 0 )
            {
                return;
            }
        }
        EndMinigame();
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
    }

    private Stone[] stones;
}

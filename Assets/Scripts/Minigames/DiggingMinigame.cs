using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingMinigame : BaseMinigame
{
    public RectTransform stoneContainer;

    protected override void InitializeMinigame(GameManager gameManager)
    {
        base.InitializeMinigame(gameManager);
        stones = stoneContainer.GetComponentsInChildren<Stone>();
    }

    public override void StartMinigame(GameManager gameManager)
    {
        if ( !this.gameManager )
            InitializeMinigame(gameManager);

        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].Restart(this);
        }

        base.StartMinigame(gameManager);
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

        inGame = false;
        anim.SetBool("Show", false);
    }

    protected override void EndMinigame()
    {
        base.EndMinigame();
    }

    private Stone[] stones;
}

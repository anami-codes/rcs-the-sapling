using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinigame : MonoBehaviour
{
    protected GameManager gameManager;
    protected Animator anim;

    protected virtual void InitializeMinigame(GameManager gameManager)
    {
        this.gameManager = gameManager;
        anim = GetComponent<Animator>();
    }

    public virtual void StartMinigame(GameManager gameManager)
    {
        anim.SetBool("Show", true);
        inGame = true;
    }

    public virtual void MinigameUpdate(float delta)
    {
        
    }

    public virtual void CheckConditions()
    {
    }

    protected virtual void EndMinigame()
    {
        gameManager.EndMinigame();
    }

    protected bool inGame = false;
}

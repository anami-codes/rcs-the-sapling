using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinigame : MonoBehaviour
{
    protected GameManager gameManager;

    public virtual void StartMinigame(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void CheckConditions()
    {

    }

    public virtual void EndMinigame()
    {
        gameManager.StartMinigame(0);
    }
}

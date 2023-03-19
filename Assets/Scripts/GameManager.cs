using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] screens;

    void Start()
    {
        sceneManager = gameObject.GetComponent<SceneManager>();
    }

    void Update()
    {
        sceneManager.GameUpdate( Time.deltaTime );
    }

    public void StartMinigame (int minigameId)
    {
        for ( int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive( i == minigameId );
        }

        if ( minigameId > 0 )
        {
            screens[minigameId].GetComponent<BaseMinigame>().StartMinigame(this);
        }
    }

    public void ChangeState ()
    {
        if (gameStage == 0)
        {
            gameStage++;
            isNight = false;
            sceneManager.ChangeState(gameStage, isNight, false);
            return;
        } 
        else if (gameStage == 3)
        {
            gameStage = 0;
            isNight = false;
            sceneManager.ChangeState(gameStage, isNight);
            return;
        }

        if (isNight)
        {
            gameStage++;
            isNight = false;
        }
        else
        {
            isNight = true;
        }

        sceneManager.ChangeState(gameStage, isNight);
    }

    private SceneManager sceneManager;

    private int gameStage = 0;
    private bool isNight = false;
}

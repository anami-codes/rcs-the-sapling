using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] screens;

    public float phaseWaitTime = 10.0f;
    public float animWaitTime = 0.25f;

    void Start()
    {
        sceneManager = gameObject.GetComponent<SceneManager>();
        uiManager = gameObject.GetComponent<UIManager>();
        timer = 0.5f;
        gameState = GameState.Animation;
    }

    void Update()
    {
        if( timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f) OnTimerEnd();
        }

        sceneManager.GameUpdate( Time.deltaTime );
    }

    public void StartMinigame()
    {
        int minigame = (gameStage % 2 != 0) ? 1 : 2;
        StartMinigame(minigame);
    }

    private void StartMinigame (int minigameId)
    {
        for ( int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive( i == minigameId );
        }

        if ( minigameId > 0 )
        {
            gameState = GameState.Minigame;
            screens[minigameId].GetComponent<BaseMinigame>().StartMinigame(this);
        }
    }

    public void EndMinigame()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }

        ChangeState();
    }

    public void ChangeState ()
    {
        gameState = GameState.Wait;

        if (gameStage == 0)
        {
            gameStage++;
            isNight = false;
            fade = false;
            timer = 2f + animWaitTime;
            return;
        } 
        else if (gameStage == 4 && isNight)
        {
            gameStage = 0;
            isNight = false;
            sceneManager.scene.animator.Play("Idle", 0);
            fade = true;
            timer = animWaitTime;
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

        fade = true;
        timer = animWaitTime;
    }

    private void OnTimerEnd()
    {
        switch(gameState)
        {
            case GameState.Wait:
                gameState = GameState.Animation;
                timer = phaseWaitTime;
                sceneManager.ChangeState(gameStage, isNight, fade);
                return;
            case GameState.Animation:
                if (gameStage == 0)
                    uiManager.ShowUI("Play");
                else
                    uiManager.ShowUI(( isNight || gameStage >= 3 ) ? "Forward" : "Game");
                return;

        }
    }

    private SceneManager sceneManager;
    private UIManager uiManager;

    private int gameStage = 0;
    private bool isNight = false;
    private bool fade = true;

    private float timer = 0.0f;

    public enum GameState
    {
        None,
        Wait,
        Animation,
        Minigame
    }
    private GameState gameState = GameState.Wait;
}

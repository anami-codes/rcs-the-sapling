using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] screens;

    void Start()
    {
        
    }

    void Update()
    {
        
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
}

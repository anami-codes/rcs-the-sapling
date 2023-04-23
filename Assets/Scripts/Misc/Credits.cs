using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameManager gameManager;

    public void StartTimer()
    {
        timer = 5.0f;
        gameManager.RestartGame();
    }

    void Update()
    {
        if( timer > 0.0f )
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
                GetComponent<Animator>().SetBool("Show", false);
        }
    }

    private float timer = 0.0f;
}

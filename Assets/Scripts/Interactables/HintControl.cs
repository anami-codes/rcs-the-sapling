using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintControl : MonoBehaviour
{
    public float minWaitTime;
    public float maxWaitTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isActive = false;
    }

    public void StartHint()
    {
        anim.Play("Action");
        timer = Random.Range(minWaitTime, maxWaitTime);
        isActive = true;
    }

    public void StopHint ()
    {
        isActive = false;
    }

    public void GameUpdate(float delta)
    {
        if(isActive && timer > 0.0f && anim.isActiveAndEnabled)
        {
            timer -= delta;
            if (timer <= 0.0f)
            {
                anim.Play("Action");
                timer = Random.Range(minWaitTime, maxWaitTime);
            }
        }
    }

    private Animator anim;
    private bool isActive;
    private float timer;
}

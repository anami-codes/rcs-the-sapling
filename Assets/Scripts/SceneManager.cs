using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Scene scene;

    public void GameUpdate (float delta)
    {
    }

    public void ChangeState(int gameStage, bool isNight, bool fade = true)
    {
        UpdateElements(GetIndex(fade), gameStage, isNight);

        if ( fade )
            scene.animator.SetTrigger("Fade");
    }

    private int GetIndex(bool fade)
    {
        if (scene.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && fade)
            return 1;
        else if (scene.animator.GetCurrentAnimatorStateInfo(0).IsName("Wait_1"))
            return 0;
        else if (scene.animator.GetCurrentAnimatorStateInfo(0).IsName("Wait_2"))
            return 1;

        return 0;
    }

    private void UpdateElements( int index, int gameStage, bool isNight )
    {
        string stateName = "Stage" + gameStage + "_";
        stateName += (isNight) ? "Night" : "Day";
        scene.tree[index].Play(stateName, 0);
        scene.root[index].Play(stateName, 0);
        scene.background[index].Play(stateName, 0);
    }
}

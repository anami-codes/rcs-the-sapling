using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UI ui;

    public void GameUpdate(float delta)
    {
    }

    public void HideUI (string panelName)
    {
        ChangeState(panelName, false);
    }

    public void ShowUI (string panelName)
    {
        ChangeState(panelName, true);
    }

    private void ChangeState(string panelName, bool newState)
    {
        switch (panelName)
        {
            case "Play":
                ui.playPanelAnim.SetBool("Show", newState);
                return;
            case "Forward":
                ui.forwardPanelAnim.SetBool("Show", newState);
                return;
            case "Game":
                ui.gamePanelAnim.SetBool("Show", newState);
                StartMinigame();
                return;
            case "Credits":
                ui.creditsAnim.SetBool("Show", newState);
                return;
        }
    }

    private void StartMinigame()
    {

    }
}

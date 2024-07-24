using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Utils
{
    public class ButtonHelper : MonoBehaviour
    {

        public void SetOffTrigger(string triggerID)
        {
            if (Game.InGame)
                Game.SetOffTrigger(triggerID);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void PlaySound(string sfxId)
        {
            SoundManager.instance.PlaySound(sfxId);
        }
    }
}
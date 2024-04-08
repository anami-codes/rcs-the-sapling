using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;

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
    }
}
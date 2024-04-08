using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.InternalStructure
{
    [System.Serializable]
    public class Action
    {
        public virtual void Execute()
        {

        }

        public virtual void GameUpdate(float delta)
        {

        }
    }
}
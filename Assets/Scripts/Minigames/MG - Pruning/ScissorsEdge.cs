using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.Pruning
{
    public class ScissorsEdge : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TargetLeaf hit = collision.transform.GetComponent<TargetLeaf>();
            if (hit != null && !hit.isReady)
            {
                hit.Cut();
                //anim.SetBool("inAction", true);
            }
        }
    }
}
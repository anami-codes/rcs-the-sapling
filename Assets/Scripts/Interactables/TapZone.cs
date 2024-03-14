using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class TapZone : Interactable
    {
        public TapZone(GameObject obj, HintControl hint) : base (InteractionType.Click ,obj, hint)
        {
            chapterManager = GameObject.Find("ChapterManager").GetComponent<Act1Chapter1>();
        }

        public override void OnInteraction(float delta)
        {
            chapterManager.Invoke("AdvanceSequence", 0.5f);
        }

        protected Act1Chapter1 chapterManager;
    }
}
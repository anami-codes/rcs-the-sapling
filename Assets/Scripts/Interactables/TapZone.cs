using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Interactables
{
    public class TapZone : Interactable
    {
        public TapZone(GameObject obj, HintControl hint, string triggerID) 
            : base (InteractionType.Click , obj, hint)
        {
            this.triggerID = triggerID;
        }

        public override void OnInteraction(float delta)
        {
            Game.SetOffTrigger(triggerID);
            hint.StopHint();
            gameObject.SetActive(false);
        }
    }
}
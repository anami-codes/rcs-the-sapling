using RainbowCat.TheSapling.Interactables;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigames
{
    public class TouchTarget : MinigameTarget
    {
        [Header("Touch")]
        public string triggerID;

        public override void SetOffTrigger(string triggerID, bool isActive, Interactable other)
        {
            if (!isReady && this.triggerID == triggerID)
            {
                SetAsReady();
                gameObject.SetActive(false);
                if(other.interactionType == Interactable.InteractionType.Drag)
                {
                    Draggable draggable = other as Draggable;
                    draggable.StopMovement(0.3f);
                }
            }
        }
    }
}
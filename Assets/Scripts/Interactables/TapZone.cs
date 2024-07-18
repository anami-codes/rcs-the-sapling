using UnityEngine;
using RainbowCat.TheSapling.Minigames;

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
            hint?.StopHint();

            if (gameObject.GetComponent<PlantingCollider>())
                gameObject.GetComponent<PlantingCollider>().ZoomIn();

            if (gameObject.GetComponent<PlantingGoal>())
                gameObject.GetComponent<PlantingGoal>().Dig();

            if (gameObject.GetComponent<FakeTarget>())
                gameObject.GetComponent<FakeTarget>().Lift();

            if (!gameObject.GetComponent<PlantingGoal>() && !gameObject.GetComponent<FakeTarget>())
                gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames
{
    public class PottedPlant : MinigameTarget
    {
        public PlantingMinigame plantingMinigame;
        public SpriteRenderer pot;

        public override void Initialize(Minigame minigame)
        {
            base.Initialize(minigame);
            interactable = new Interactable(Interactable.InteractionType.Drag, gameObject, null);
            initialPos = transform.position;
        }

        public override void GameUpdate(float delta)
        {
            base.GameUpdate(delta);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlantingGoal"))
            {
                PlantingGoal goal = collision.GetComponent<PlantingGoal>();
                interactable.InterruptAction();
                if (goal.target == this && goal.CanPlant())
                    Success(goal);
                else
                    Error();
            }
        }

        private void Success(PlantingGoal goal)
        {
            interactable.SetStatus(false);
            transform.position = goal.transform.position;
            //goal.gameObject.SetActive(false);
            pot.enabled = false;
            plantingMinigame.ZoomOut();
            SetAsReady();
        }

        private void Error()
        {
            transform.position = initialPos;
        }

        private Vector3 initialPos;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.Planting
{
    public class PottedPlant : MinigameTarget
    {
        public SpriteRenderer pot;

        protected override void Initialize()
        {
            base.Initialize();
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
                interactable.InterruptInteraction();
                if (goal.target == this)
                    Success(goal);
                else
                    Error();
            }
        }

        private void Success(PlantingGoal goal)
        {
            interactable.SetStatus(false);
            transform.position = goal.transform.position;
            goal.gameObject.SetActive(false);
            pot.enabled = false;
            SetAsReady();
        }

        private void Error()
        {
            transform.position = initialPos;
        }

        private Vector3 initialPos;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.Collecting
{
    public class TargetCollectible : MinigameTarget
    {
        public bool isDummy = false;

        protected override void Initialize()
        {
            base.Initialize();
            interactable = new Interactable(Interactable.InteractionType.Drag, gameObject, null);
            isNecessary = !isDummy;
        }

        public override void GameUpdate(float delta)
        {
            base.GameUpdate(delta);
            if (isDummy && interactable.inAction)
                Error();
            else
                interactable.GameUpdate(delta);
        }

        private void Error()
        {
            //Error fedback
            interactable.InterruptInteraction();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("CollectionGoal"))
            {
                gameObject.SetActive(false);
                SetAsReady();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
        }
    }
}
using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames
{
    public class TargetCollectible : MinigameTarget
    {
        public bool isDummy = false;

        public override void Initialize(Minigame minigame)
        {
            base.Initialize(minigame);
            interactable = new Interactable(Interactable.InteractionType.Drag, gameObject, null);
            isNecessary = !isDummy;
        }

        public override void GameUpdate(float delta)
        {
            if (isDummy && interactable.inAction)
                Error();
            else
                interactable.GameUpdate(delta);
        }

        private void Error()
        {
            anim.SetTrigger("Failure");
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
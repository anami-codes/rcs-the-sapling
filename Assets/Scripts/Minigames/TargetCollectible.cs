using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames
{
    public class TargetCollectible : MinigameTarget
    {
        public Sprite collectedSprite;
        public bool isDummy = false;

        public override void Initialize(Minigame minigame)
        {
            base.Initialize(minigame);
            interactable = new Interactable(Interactable.InteractionType.Drag, gameObject, null, "sfx_mg_pick_berry");
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
            SoundManager.instance.PlaySound(errorSFX);
            interactable.InterruptAction();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("CollectionGoal"))
            {
                gameObject.SetActive(false);
                collision.GetComponent<CollectibleGoal>().AddSprite(collectedSprite);
                SetAsReady();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
        }
    }
}
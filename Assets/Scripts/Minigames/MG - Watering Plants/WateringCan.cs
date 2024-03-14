using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.WateringPlants
{

    public class WateringCan : Interactable
    {
        public WateringCan (GameObject obj, HintControl hint) : base(InteractionType.Drag, obj, hint)
        {
        }

        public override Interactable StartInteraction(InputController controller)
        {
            base.StartInteraction(controller);
            anim.SetBool("inMovement", true);
            return this;
        }

        public override void OnInteraction(float delta)
        {
            base.OnInteraction(delta);

            if (inAction && target != null)
            {
                target.Water(delta);
                if(target.isReady)
                    anim.SetBool("inAction", false);
            }
        }

        public override void EndInteraction()
        {
            base.EndInteraction();
            anim.SetBool("inMovement", false);
        }

        public override void InterruptInteraction()
        {
            base.InterruptInteraction();
        }

        public override void TriggerEnter(Collider2D collision)
        {
            TargetPlant hit = collision.transform.GetComponent<TargetPlant>();
            if (hit != null && !hit.isReady )
            {
                target = hit;
                anim.SetBool("inAction", true);
            }
        }

        public override void TriggerExit(Collider2D collision)
        {
            TargetPlant hit = collision.transform.GetComponent<TargetPlant>();
            if (hit == target)
            {
                target = null;
                anim.SetBool("inAction", false);
            }
        }

        private TargetPlant target;
    }
}
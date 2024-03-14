using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame.Pruning
{
    public class Scissors : Interactable
    {
        public Scissors(GameObject obj, HintControl hint) : base(InteractionType.Drag, obj, hint)
        {
        }

        public override Interactable StartInteraction(InputController controller)
        {
            base.StartInteraction(controller);
            return this;
        }

        public override void OnInteraction(float delta)
        {
            base.OnInteraction(delta);
        }
    }
}
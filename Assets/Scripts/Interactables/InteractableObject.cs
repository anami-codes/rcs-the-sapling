using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class InteractableObject : MonoBehaviour
    {
        public HintControl hint;

        public enum Type
        {
            TapZone,
            WateringCan,
            Scissors
        }
        public Type type;
        public Interactable interactable { get; protected set; }

        void Awake()
        {
            switch(type)
            {
                case Type.TapZone:
                    interactable = new TapZone(gameObject, hint);
                    break;
                case Type.WateringCan:
                    interactable = new Minigame.WateringPlants.WateringCan(gameObject, hint);
                    break;
                case Type.Scissors:
                    interactable = new Minigame.Pruning.Scissors(gameObject, hint);
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            interactable.TriggerEnter(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            interactable.TriggerExit(collision);
        }
    }
}
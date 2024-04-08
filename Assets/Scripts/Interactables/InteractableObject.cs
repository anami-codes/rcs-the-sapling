using UnityEngine;

namespace RainbowCat.TheSapling.Interactables
{
    public class InteractableObject : MonoBehaviour
    {
        public HintControl hint;
        public bool alwaysShowHint;

        public enum Type
        {
            TapZone,
            Draggable,
        }
        public Type type;
        public string interactionCode;

        public void Initialize()
        {
            switch(type)
            {
                case Type.TapZone:
                    interactable = new TapZone(gameObject, hint, interactionCode);
                    break;
                case Type.Draggable:
                    interactable = new Draggable(gameObject, hint, interactionCode);
                    break;
            }
        }

        public void Activate(bool isActive)
        {
            gameObject.SetActive(isActive);
            if (hint != null && alwaysShowHint && isActive)
                hint.StartHint();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            interactable.TriggerEnter(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            interactable.TriggerStay(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            interactable.TriggerExit(collision);
        }

        public Interactable interactable { get; protected set; }
    }
}
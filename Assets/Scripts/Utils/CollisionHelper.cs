using UnityEngine;
using RainbowCat.TheSapling.Interactables;
using RainbowCat.TheSapling.Minigames;

namespace RainbowCat.TheSapling.Utils
{
    public class CollisionHelper : MonoBehaviour
    {
        public InteractableObject interactableObj;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            interactableObj.TriggerEnter(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            interactableObj.TriggerStay(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            interactableObj.TriggerExit(collision);
        }
    }
}
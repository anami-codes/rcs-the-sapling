using UnityEngine;

namespace RainbowCat.TheSapling.Minigames.Tools
{
    public class ChargingPoint : MonoBehaviour
    {
        public Interactables.InteractableObject interactableObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Charger charger = collision.GetComponent<Charger>();
            if (charger != null)
            {
                if (interactableObject.CheckCharger(charger.chargerType))
                {
                    interactableObject.interactable.InterruptAction();
                    charger.StartCharging(interactableObject);
                }
            }
        }
    }
}
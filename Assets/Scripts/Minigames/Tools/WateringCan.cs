using UnityEngine;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames.Tools
{
    public class WateringCan : InteractableObject
    {
        [Header("Watering Can")]
        public Transform gaugeObj;

        private void Start()
        {
            chargerType = Charger.ChargerType.Faucet;
            Vector3 gaugeScale = gaugeObj.localScale;
            gaugeScale.y = 0.0f;
            gaugeObj.localScale = gaugeScale;
        }

        private void Update()
        {
            if (interactable.isInteracting) UseCharge(Time.deltaTime);

            if(m_charging || interactable.isInteracting)
            {
                Vector3 gaugeScale = gaugeObj.localScale;
                gaugeScale.y = Mathf.Lerp(0.0f, gaugeMax, currentCharge);
                gaugeObj.localScale = gaugeScale;
            }
        }

        private float gaugeMax = 0.2f;
    }
}
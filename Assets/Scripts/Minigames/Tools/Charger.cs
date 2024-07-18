using UnityEngine;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames.Tools
{
    public class Charger : MonoBehaviour
    {
        public enum ChargerType
        {
            Faucet
        }

        public ChargerType chargerType;
        public Transform chargingPos;
        public float chargingTime = 1.0f;

        public void StartCharging(InteractableObject target)
        {
            m_target = target;
            m_startPos = m_target.transform.position;
            m_lerpT = 0.0f;
            m_movingTarget = true;
        }

        private void Update()
        {
            if(m_movingTarget)
            {
                m_lerpT += Time.deltaTime;
                Vector3 newPos = Vector3.Lerp(m_startPos, chargingPos.position, m_lerpT);
                m_target.transform.position = newPos;
                m_movingTarget = (m_lerpT < 1.0f);
                m_chargingTarget = !m_movingTarget;
            }

            if (m_chargingTarget) ChargeTarget(Time.deltaTime);
        }

        private void ChargeTarget(float delta)
        {
            float currentCharge = m_target.Charge(delta/chargingTime);
            m_chargingTarget = currentCharge < 1.0f;
        }

        private bool m_movingTarget = false;
        private bool m_chargingTarget = false;
        
        private InteractableObject m_target;

        private float m_lerpT = 0.0f;
        private Vector3 m_startPos;
    }
}
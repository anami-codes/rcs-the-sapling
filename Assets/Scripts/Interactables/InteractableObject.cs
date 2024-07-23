using UnityEngine;
using RainbowCat.TheSapling.Minigames.Tools;

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
        public float maxCharge = 5.0f;
        public string sfxClip;

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

        public bool CheckCharger(Charger.ChargerType chargerType)
        {
            if (maxCharge <= 0.0f) return false;

            if ((this.chargerType == chargerType) && currentCharge <= 0.5f)
            {
                m_charging = true;
                interactable.SetStatus(false);
            }
            else
            {
                m_charging = false;
            }
            
            return m_charging;
        }

        public float Charge(float t)
        {
            m_currentCharge += (t * maxCharge);
            if(m_currentCharge >= maxCharge)
            {
                m_currentCharge = maxCharge;
                m_charging = false;
                interactable.SetStatus(true);
            }

            return currentCharge;
        }

        public void UseCharge(float t)
        {
            if (maxCharge <= 0.0f || m_currentCharge <= 0.0f) return;

            m_currentCharge -= t;
            if (m_currentCharge < 0.0f)
            {
                m_currentCharge = 0.0f;
                interactable.InterruptInteraction();
            }
        }

        public void TriggerEnter(Collider2D collision) { OnTriggerEnter2D(collision); }
        public void TriggerStay(Collider2D collision) { OnTriggerStay2D(collision); }
        public void TriggerExit(Collider2D collision) { OnTriggerExit2D(collision); }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (maxCharge > 0.0f && m_currentCharge <= 0.0f) return;
            interactable.TriggerEnter(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (maxCharge > 0.0f && m_currentCharge <= 0.0f) return;
            interactable.TriggerStay(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (maxCharge > 0.0f && m_currentCharge <= 0.0f) return;
            interactable.TriggerExit(collision);
        }

        public Interactable interactable { get; protected set; }

        protected float currentCharge { get { return m_currentCharge / maxCharge; } }
        protected float m_currentCharge = 0.0f;
        protected bool m_charging = false;
        protected Charger.ChargerType chargerType;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using RainbowCat.TheSapling.Minigames;

namespace RainbowCat.TheSapling.Interactables
{
    public class Draggable : Interactable
    {
        public Draggable(GameObject obj, HintControl hint, string triggerID)
            : base(InteractionType.Drag, obj, hint)
        {
            this.triggerID = triggerID;
            m_interactionStopped = false;
        }

        public override void GameUpdate(float delta)
        {
            if (m_freezeTimer > 0.0f)
            {
                m_freezeTimer -= delta;
                if (m_freezeTimer <= 0.0f && inAction)
                    Mouse.current.WarpCursorPosition(m_freezePos);
            } 
            else
            {
                base.GameUpdate(delta);
            }
        }

        public void StopMovement(float seconds)
        {
            m_freezePos = Input.mousePosition;
            m_freezeTimer = seconds;
        }

        public override Interactable StartInteraction(InputController controller)
        {
            base.StartInteraction(controller);
            anim.SetBool("inMovement", true);
            if (hint != null && hint.isActive) 
                hint.StopHint();
            return this;
        }

        public override void EndInteraction()
        {
            base.EndInteraction();
            StopAction();
        }

        public override void InterruptInteraction()
        {
            base.InterruptInteraction();
            StopAction();
        }

        public override void TriggerEnter(Collider2D collision)
        {
            MinigameTarget target = collision.GetComponent<MinigameTarget>();
            if (target)
            {
                m_target = target;
                m_target.SetOffTrigger(triggerID, true, this);
                anim.SetBool("inAction", true);
            }
        }

        public override void TriggerStay(Collider2D collision)
        {
            if (m_interactionStopped)
            {
                MinigameTarget target = collision.GetComponent<MinigameTarget>();
                if (target)
                {
                    if (m_target != target)
                    {
                        m_target.SetOffTrigger(triggerID, false, this);
                        m_target = target;
                    }

                    m_target.SetOffTrigger(triggerID, true, this);
                    anim.SetBool("inMovement", true);
                    anim.SetBool("inAction", true);
                    m_interactionStopped = false;
                }
            }
        }

        public override void TriggerExit(Collider2D collision)
        {
            MinigameTarget target = collision.GetComponent<MinigameTarget>();
            if (target)
            {
                target.SetOffTrigger(triggerID, false, this);
                anim.SetBool("inAction", false);
                m_target = null;
            }
        }

        private void StopAction()
        {
            anim.SetBool("inAction", false);
            anim.SetBool("inMovement", false);
            m_target?.SetOffTrigger(triggerID, false, this);
            m_interactionStopped = true;
        }

        private MinigameTarget m_target;
        private bool m_interactionStopped;

        private Vector2 m_freezePos;
        private float m_freezeTimer;
    }
}
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

                if (isInteracting && ( (m_target && m_target.isReady) || (!inAction) ) )
                {
                    m_target?.SetOffTrigger(triggerID, false, this);
                    anim.SetBool("inAction", false);
                    isInteracting = false;
                    m_target = null;
                }
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
            Game.manager.mouse.ChangeVisibility(false);
            if (hint != null && hint.isActive) 
                hint.StopHint();
            return this;
        }

        public override void EndAction()
        {
            base.EndAction();
            StopAction();
        }

        public override void InterruptAction()
        {
            base.InterruptAction();
            StopAction();
        }

        public override void InterruptInteraction()
        {
            base.InterruptInteraction();
            m_target?.SetOffTrigger(triggerID, false, this);
            anim.SetBool("inAction", false);
            isInteracting = false;
            m_target = null;
        }

        public override void TriggerEnter(Collider2D collision)
        {
            MinigameTarget target = collision.GetComponent<MinigameTarget>();
            if (target && !target.isReady)
            {
                m_target = target;
                m_target.SetOffTrigger(triggerID, true, this);
                anim.SetBool("inAction", true);
                isInteracting = true;
            }
        }

        public override void TriggerStay(Collider2D collision)
        {
            MinigameTarget target = collision.GetComponent<MinigameTarget>();
            if (target && !target.isReady)
            {
                if (m_target != target)
                {
                    m_target?.SetOffTrigger(triggerID, false, this);
                    m_target = target;
                }

                m_target.SetOffTrigger(triggerID, true, this);
                anim.SetBool("inMovement", true);
                anim.SetBool("inAction", true);
                isInteracting = true;
            }
        }

        public override void TriggerExit(Collider2D collision)
        {
            MinigameTarget target = collision.GetComponent<MinigameTarget>();
            if (target && !target.isReady)
            {
                target.SetOffTrigger(triggerID, false, this);
                anim.SetBool("inAction", false);
                isInteracting = false;
                m_target = null;
            }
        }

        private void StopAction()
        {
            anim.SetBool("inAction", false);
            anim.SetBool("inMovement", false);
            m_target?.SetOffTrigger(triggerID, false, this);
            Game.manager.mouse.ChangeVisibility(true);
            isInteracting = false;
        }

        private MinigameTarget m_target;

        private Vector2 m_freezePos;
        private float m_freezeTimer;
    }
}
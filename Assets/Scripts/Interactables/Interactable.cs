using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class Interactable
    {
        public HintControl hint;
        public GameObject gameObject { get; protected set; }
        public bool inAction { get; protected set; }

        public enum InteractionType
        {
            Click,
            Drag,
            Hold
        }
        protected InteractionType interactionType;

        public Interactable( InteractionType type, GameObject obj, HintControl hint = null)
        {
            gameObject = obj;
            interactionType = type;
            this.hint = hint;
            anim = obj.GetComponent<Animator>();
            inAction = false;
        }

        public virtual Interactable StartInteraction(InputController controller)
        {
            if (interactionType == InteractionType.Click)
            {
                OnInteraction(0.0f);
                return null;
            }
            else
            {
                inputController = controller;
                inAction = true;

                if (interactionType == InteractionType.Hold)
                {
                    counter = 0;
                }
                return this;
            }
        }

        public virtual void OnInteraction(float delta)
        {
            if (interactionType == InteractionType.Drag && inAction)
            {
                Vector3 newPos = gameObject.transform.position;
                newPos.x = inputController.mousePosition.x;
                newPos.y = inputController.mousePosition.y;
                gameObject.transform.position = newPos;
            }

            if (interactionType == InteractionType.Hold && inAction)
            {
                counter += delta;
            }
        }

        public virtual void EndInteraction()
        {
            inAction = false;
            inputController = null;
        }

        public virtual void InterruptInteraction()
        {
            inAction = false;
            inputController = null;
        }

        public virtual void GameUpdate(float delta)
        {
            if( inAction ) OnInteraction(delta);
            if (hint != null && hint.gameObject.activeSelf) 
                hint.GameUpdate(delta);
        }

        public virtual void TriggerEnter(Collider2D collision)
        {
        }

        public virtual void TriggerExit(Collider2D collision)
        {
        }

        protected InputController inputController;
        protected Animator anim;
        protected float counter = 0.0f;
    }
}
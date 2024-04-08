using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.Minigames
{
    public class MinigameTarget : MonoBehaviour
    {
        public Interactable interactable { get; protected set; }
        public bool isReady { get; private set; }
        public bool isNecessary = true;

        public virtual void Initialize (Minigame minigame)
        {
            anim = GetComponentInChildren<Animator>();
            this.minigame = minigame;
            isReady = false;
            m_timer = 0.0f;
            m_stage = 0;
        }

        public virtual void GameUpdate( float delta )
        {
            if(interactable != null)
                interactable.GameUpdate(delta);
        }

        public virtual void SetOffTrigger(string triggerID, bool isActive, Interactable other)
        {

        }

        protected void SetAsReady()
        {
            isReady = true;
            minigame.CheckTargets();
        }

        protected Minigame minigame;
        protected Animator anim;
        protected float m_timer;
        protected int m_stage;
    }
}
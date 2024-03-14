using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigame
{
    public class MinigameTarget : MonoBehaviour
    {
        public Interactable interactable { get; protected set; }
        public bool isReady { get; private set; }
        public bool isNecessary = true;

        void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize ()
        {
            anim = GetComponentInChildren<Animator>();
            minigameManager = GetComponentInParent<MinigameManager>();
            minigameManager.AddTarget(this);
            isReady = false;
            timer = 0.0f;
            stage = 1;
        }

        public virtual void GameUpdate( float delta )
        {

        }

        protected void SetAsReady()
        {
            isReady = true;
            minigameManager.CheckTargets();
        }

        protected MinigameManager minigameManager;
        protected Animator anim;
        protected float timer;
        protected int stage;
    }
}
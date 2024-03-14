using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class PageInfo : MonoBehaviour
    {
        public string id;
        public RectTransform ui;
        public bool isMinigame { get; protected set; }
        public Camera cam { get; protected set; }
        public Animator anim { get; protected set; }
        public Minigame.MinigameManager minigameManager { get; protected set; }
        public InteractableObject[] interactables { get; protected set; }

        private void Awake()
        {
            cam = GetComponentInChildren<Camera>();
            anim = GetComponent<Animator>();
            interactables = GetComponentsInChildren<InteractableObject>();
            minigameManager = GetComponent<Minigame.MinigameManager>();
            isMinigame = (minigameManager != null);
            if (isMinigame) minigameManager.SetPage(this);
            chapterManager = FindAnyObjectByType<ChapterManager>();

            foreach (InteractableObject obj in interactables)
            {
                obj.gameObject.SetActive(false);
            }
        }

        public void ActivateInteractables(bool activate)
        {
            foreach (InteractableObject obj in interactables)
            {
                obj.gameObject.SetActive(activate);
            }
        }

        public InteractableObject GetInteractable (string objName)
        {
            for(int i = 0; i < interactables.Length; i++)
            {
                if (interactables[i].gameObject.name == objName)
                    return interactables[i];
            }
            return null;
        }

        public void GameUpdate(float delta)
        {
            foreach( InteractableObject obj in interactables)
            {
                obj.interactable.GameUpdate(delta);
            }

            if (minigameManager != null) minigameManager.GameUpdate(delta);
        }

        public void Finish()
        {
            foreach (InteractableObject obj in interactables)
            {
                obj.interactable.InterruptInteraction();
            }

            if ( isMinigame ) chapterManager.EndMinigame();
        }

        protected ChapterManager chapterManager;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class SceneManagerTemp : MonoBehaviour
    {
        public Minigame.MinigameTarget[] targets { get; private set; }
        public InteractableObject[] interactableObjects { get; private set; }

        void Awake()
        {
            anim = GetComponent<Animator>();
            GameObject interactableParent = GameObject.Find("Interactables");
            if (interactableParent)
                interactableObjects = interactableParent.GetComponentsInChildren<InteractableObject>();
            else
                interactableObjects = new InteractableObject[0];

            GameObject targetParent = GameObject.Find("Targets");
            if (targetParent)
                targets = targetParent.GetComponentsInChildren<Minigame.MinigameTarget>();
            else
                targets = new Minigame.MinigameTarget[0];
        }

        public void ChangeScene(string newState)
        {
            anim.Play(newState);
        }

        Animator anim;
    }
}
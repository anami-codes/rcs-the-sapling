using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class InputController : MonoBehaviour
    {
        public Vector2 mousePosition { get; private set; }

        void Start()
        {
        }

        void Update()
        {
            mousePosition = GetMousePosition();

            if (GameManager.Status == GameManager.GameStatus.WAITING_FOR_INPUT ||
                GameManager.Status == GameManager.GameStatus.IN_MINIGAME)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    int layerObject = 2;
                    Vector2 ray = mousePosition;
                    RaycastHit2D hit = Physics2D.Raycast(ray, ray, layerObject);
                    if (hit.collider != null)
                    {
                        GameObject hitObj = hit.collider.gameObject;
                        if (hitObj.GetComponent<InteractableObject>())
                            interactable = hitObj.GetComponent<InteractableObject>().interactable;
                        else if (hitObj.GetComponent<Minigame.MinigameTarget>())
                            interactable = hitObj.GetComponent<Minigame.MinigameTarget>().interactable;

                        interactable?.StartInteraction(this);
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (interactable != null)
                        interactable.EndInteraction();
                }
            }
        }

        private Vector2 GetMousePosition()
        {
            float mouseX = GameManager.instance.currentCamera.ScreenToWorldPoint(Input.mousePosition).x;
            float mouseY = GameManager.instance.currentCamera.ScreenToWorldPoint(Input.mousePosition).y;
            return new Vector2(mouseX, mouseY);
        }

        private Interactable interactable = null;
    }
}
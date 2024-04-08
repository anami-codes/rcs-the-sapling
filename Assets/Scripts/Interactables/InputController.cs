using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Interactables
{
    public class InputController
    {
        public Vector2 mousePosition { get; private set; }

        public void GameUpdate(float delta)
        {
            mousePosition = GetMousePosition();

            if (Game.manager.State == Game.State.WAITING_FOR_INPUT ||
                Game.manager.State == Game.State.IN_MINIGAME)
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
                        else if (hitObj.GetComponent<Minigames.MinigameTarget>())
                            interactable = hitObj.GetComponent<Minigames.MinigameTarget>().interactable;

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
            float mouseX = CameraController.gameCamera.ScreenToWorldPoint(Input.mousePosition).x;
            float mouseY = CameraController.gameCamera.ScreenToWorldPoint(Input.mousePosition).y;
            return new Vector2(mouseX, mouseY);
        }

        private Interactable interactable = null;
    }
}
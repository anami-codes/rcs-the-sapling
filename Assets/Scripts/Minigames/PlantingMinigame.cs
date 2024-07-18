using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RainbowCat.TheSapling.Minigames
{
    public class PlantingMinigame : MonoBehaviour
    {
        public SubMinigame[] subMinigames;

        public void ZoomIn(int index)
        {
            currentSubMinigame = index;
            Game.cameraController.ChangeCamera(subMinigames[currentSubMinigame].camera.name);
            subMinigames[currentSubMinigame].pottedPlant.enabled = true;
            subMinigames[currentSubMinigame].plantingGoal.enabled = true;
            foreach (SubMinigame subMinigame in subMinigames)
            {
                subMinigame.plantingCollider.gameObject.SetActive(false);
            }
        }

        public void ZoomOut()
        {
            subMinigames[currentSubMinigame].pottedPlant.enabled = false;
            subMinigames[currentSubMinigame].plantingGoal.enabled = false;
            subMinigames[currentSubMinigame].plantingCollider.isReady = true;
            currentSubMinigame = -1;
            Game.cameraController.ChangeCamera("DefaultCam");

            foreach (SubMinigame subMinigame in subMinigames)
            {
                subMinigame.plantingCollider.gameObject.SetActive(!subMinigame.plantingCollider.isReady);
            }
        }

        private void Update()
        {

        }

        private int currentSubMinigame = -1;
    }

    [System.Serializable]
    public struct SubMinigame
    {
        public PlantingCollider plantingCollider;
        public Collider2D pottedPlant;
        public Collider2D plantingGoal;
        public CinemachineVirtualCamera camera;
        public bool isReady { get { return plantingCollider.isReady; } }
    }
}
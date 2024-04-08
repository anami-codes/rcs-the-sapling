using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class CameraController
    {
        //Saving cameras in a Dictionary for easy access
        public CameraController()
        {
            m_cameras = new Dictionary<string, CinemachineVirtualCamera>();
            CinemachineVirtualCamera defaultCam = Game.manager.GetComponentInChildren<CinemachineVirtualCamera>();
            m_cameras.Add(defaultCam.name, defaultCam);
            defaultCamera = defaultCam.name;
            defaultCam.gameObject.SetActive(true);
            virtualCamera = defaultCam;
        }

        public void AddCameras(CinemachineVirtualCamera[] camList)
        {
            for (int i = 0; i < camList.Length; i++)
            {
                if( !m_cameras.ContainsKey(camList[i].name))
                {
                    m_cameras.Add(camList[i].name, camList[i]);
                    camList[i].gameObject.SetActive(false);
                }
            }
        }

        public void ChangeCamera(string camID)
        {
            virtualCamera.gameObject.SetActive(false);
            virtualCamera = m_cameras[camID];
            virtualCamera.gameObject.SetActive(true);
        }

        public void UpdatePosition(Vector2 newPos)
        {
            Vector3 pos = virtualCamera.transform.position;
            pos.y = newPos.y;
            pos.x = newPos.x;
            virtualCamera.transform.position = pos;
        }

        public static string defaultCamera { get; private set; }
        public CinemachineVirtualCamera virtualCamera { get; private set; }
        public static Camera gameCamera
        {
            get
            {
                return Game.manager.chapter.cam.GetComponent<Camera>();
            }
        }

        private Dictionary<string, CinemachineVirtualCamera> m_cameras;
    }
}
using Cinemachine;

namespace RainbowCat.TheSapling.InternalStructure
{
    public static class Game
    {
        public enum State
        {
            PLAYING_CINEMATIC,
            WAITING_FOR_INPUT,
            MINIGAME_START,
            IN_MINIGAME,
            MINIGAME_END,
            IN_TRANSITION,
            PAUSE
        }

        public static bool InGame
        {
            get
            {
                return (manager.State != State.PAUSE &&
                    manager.State != State.IN_TRANSITION);
            }
        }

        public static void SetGameManager(GameManager manager_)
        {
            manager = manager_;
        }

        public static void SetOffTrigger(string triggerID)
        {
            manager.SetOffTrigger(triggerID);
        }

        public static void LoadCameras(CinemachineVirtualCamera[] camList)
        {
            if(cameraController == null)
                cameraController = new CameraController();

            cameraController.AddCameras(camList);
        }

        public static void UpdateCenter(float x, float y)
        {
            cameraController.UpdatePosition(x, y);
            manager.inputController.UpdateCenter(x, y);
        }

        public static void Clean()
        {
            cameraController = null;
        }

        public static GameManager manager { get; private set; }
        public static CameraController cameraController { get; private set; }
    }
}
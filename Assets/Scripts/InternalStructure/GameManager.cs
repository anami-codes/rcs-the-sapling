using UnityEngine;
using UnityEngine.SceneManagement;
using RainbowCat.TheSapling.Transitions;
using RainbowCat.TheSapling.Interactables;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class GameManager : MonoBehaviour
    {
        public Game.State State { get; protected set; }
        public ChapterManager chapter { get; protected set; }
        

        public MouseHelper mouse;
        public GameObject loadingPanel;
        public Material[] gameMaterials;
        public TextAsset config;

        void Awake()
        {
            if (Game.manager == null)
            {
                Game.SetGameManager(this);
                DontDestroyOnLoad(gameObject);
                TechArt.WatercolorShader.Initialize();
                Cursor.visible = false;
            }
            else
            {
                Destroy(gameObject);
            }
            State = Game.State.WAITING_FOR_INPUT;
            GetNewSceneInfo();
            loadingPanel.SetActive(false);
            sceneName = SceneManager.GetActiveScene().name;
            loadingScene = false;
        }

        public void ChangeState(Game.State nextState)
        {
            if (State == Game.State.IN_TRANSITION)
            {
                if (chapter.currentPage.isMinigame)
                {
                    State = Game.State.WAITING_FOR_INPUT;
                }
                else
                {
                    State = Game.State.PLAYING_CINEMATIC;
                }
                Debug.Log("Current State: " + State);
            }
            else if (nextState != State)
            {
                State = nextState;
                Debug.Log("Current State: " + State);
            }
        }

        public void ChangeScene(string code, string message)
        {
            loadingScene = true;
            SceneManager.LoadScene(code, LoadSceneMode.Single);
        }

        public void ShowLoadingPanel(string code, string message)
        {
            loadingPanel.SetActive(!loadingPanel.activeSelf);
        }

        public void SetOffTrigger(string triggerID)
        {
            chapter.SetOffTrigger(triggerID);
        }

        public bool CallTransition(TransitionInfo info, Entity sender)
        {
            if (info.target == TransitionInfo.Target.FinishChapter)
                sceneName = info.code;
                
            if(info.triggerID != "")
                TransitionController.Receiver(info, sender);

            return (info.triggerID != "");
        }

        // GameUpdate function runs when game isn't paused, in transition or loading a new scene
        protected void GameUpdate(float delta)
        {
            if ( (State != Game.State.PAUSE) && (State != Game.State.IN_TRANSITION) )
            {
                Game.inputController.GameUpdate(delta);
                Utils.TimerController.GameUpdate(delta);
                if (chapter != null) chapter.GameUpdate(delta);
            }

            TransitionController.GameUpdate(delta);
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            if (!loadingScene)
                GameUpdate(delta);

            if (loadingScene && SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                sceneExtraTimer -= delta;
                if (sceneExtraTimer <= 0.0f)
                {
                    //Save progress
                    GetNewSceneInfo();
                    loadingScene = false;
                }
            }
        }

        private void GetNewSceneInfo()
        {
            chapter = FindObjectOfType<ChapterManager>();
            Cinemachine.CinemachineVirtualCamera[] cameras = FindObjectsByType<Cinemachine.CinemachineVirtualCamera>(FindObjectsSortMode.None);
            Game.LoadCameras(cameras);
            FindObjectOfType<Utils.SettingsHelper>().SetInitialValues();
        }

        private bool loadingScene;
        private string sceneName;
        private float sceneExtraTimer;
    }
}
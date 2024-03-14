using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public enum GameStatus
        {
            PLAYING_CINEMATIC,
            WAITING_FOR_INPUT,
            MINIGAME_START,
            IN_MINIGAME,
            MINIGAME_END, 
            PAUSE
        }
        public static GameStatus Status { get; protected set; }
        public ChapterManager chapterManager { get; protected set; }
        public Camera currentCamera { get; protected set; }

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Status = GameStatus.WAITING_FOR_INPUT;
        }

        public void SetChapter(ChapterManager chapterManager)
        {
            this.chapterManager = chapterManager;
        }

        public void ChangeState(GameStatus nextState)
        {
            Status = nextState;
            Debug.Log("Current State: " + Status);
        }

        public void ChangeCamera(Camera newCamera)
        {
            currentCamera = newCamera;
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            GameUpdate(delta);
        }

        protected void GameUpdate(float delta)
        {
            if (Status != GameStatus.PAUSE)
            {
                if(chapterManager != null) chapterManager.GameUpdate(delta);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class MainMenu : ChapterManager
    {
        private void Start()
        {
            currentPage = pages["MainMenu"];
            GameManager.instance.SetChapter(this);
            GameManager.instance.ChangeCamera(currentPage.cam);
        }

        public void Play()
        {
            GameManager.instance.ChangeState(GameManager.GameStatus.PLAYING_CINEMATIC);
            currentPage.ui.GetComponent<Animator>().Play("Outro");
            currentPage.anim.Play("Outro");
            ChangeScene(1, currentPage.anim);
        }
    }
}
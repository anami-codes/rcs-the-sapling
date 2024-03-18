using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling
{
    public class Act1Chapter1 : ChapterManager
    {
        protected enum Sequence
        {
            Start,
            Storyboard01,
            WateringMinigame,
            Storyboard02,
            PruningMinigame,
            Storyboard03,
            CollectingMinigame,
            Storyboard04,
            CollectingMinigame_Repeat,
            Storyboard05,
            PlantingMinigame,
            Storyboard06,
            End,

        }
        protected Sequence step;

        public override void GameUpdate(float delta)
        {
            base.GameUpdate(delta);
        }

        public override void AdvanceSequence()
        {
            switch(step)
            {
                case Sequence.Start:
                    ActionAfterTransition(ActivateInteraction, currentPage.anim);
                    step = Sequence.Storyboard01;
                    break;
                case Sequence.Storyboard01:
                    ChangeHintState("TapZone", false);
                    ChangeTo(currentPage.id, "Outro", AdvanceSequence);
                    step = Sequence.WateringMinigame;
                    break;
                case Sequence.WateringMinigame:
                    ChangeTo("Watering Plants", "Intro", StartMinigame);
                    break;
                case Sequence.Storyboard02:
                    ChangeTo("Storyboard 02", "Intro", AdvanceSequence);
                    step = Sequence.PruningMinigame;
                    break;
                case Sequence.PruningMinigame:
                    ChangeTo("Pruning Leaves", "Intro", StartMinigame);
                    break;
                case Sequence.Storyboard03:
                    ChangeTo("Storyboard 03", "Intro", AdvanceSequence);
                    step = Sequence.CollectingMinigame;
                    break;
                case Sequence.CollectingMinigame:
                    ChangeTo("Collecting Berries", "Intro", StartMinigame);
                    break;
                case Sequence.Storyboard04:
                    if(currentPage.id == "Storyboard 04")
                    {
                        ChangeHintState("TapZone", false);
                        ChangeTo(currentPage.id, "Outro", AdvanceSequence);
                        step = Sequence.CollectingMinigame_Repeat;
                    } 
                    else
                    {
                        ChangeTo("Storyboard 04", "Intro", ActivateInteraction);
                    }
                    break;
                case Sequence.CollectingMinigame_Repeat:
                    ChangeTo("Collecting Berries 2", "Intro", StartMinigame);
                    break;
                case Sequence.Storyboard05:
                    ChangeTo("Storyboard 05", "Intro", AdvanceSequence);
                    step = Sequence.PlantingMinigame;
                    break;
                case Sequence.PlantingMinigame:
                    ChangeTo("Planting", "Intro", StartMinigame);
                    break;
                case Sequence.Storyboard06:
                    if (currentPage.id == "Storyboard 06")
                    {
                        ChangeHintState("TapZone", false);
                        ChangeTo(currentPage.id, "Outro", AdvanceSequence);
                        step = Sequence.End;
                    }
                    else
                    {
                        ChangeTo("Storyboard 06", "Intro", ActivateInteraction);
                    }
                    break;
                default:
                    Debug.Log("<color=red>ERROR: Couldn't find next step in sequence</color>");
                    break;
            }
        }

        private void Start() {
            currentPage = pages["Storyboard 01"];
            GameManager.instance.SetChapter(this);
            ChangeCamera(currentPage.id);
            step = Sequence.Start;
            AdvanceSequence();
        }

        public override void EndMinigame()
        {
            switch (step)
            {
                case Sequence.WateringMinigame:
                    step = Sequence.Storyboard02;
                    break;
                case Sequence.PruningMinigame:
                    step = Sequence.Storyboard03;
                    break;
                case Sequence.CollectingMinigame:
                    step = Sequence.Storyboard04;
                    break;
                case Sequence.CollectingMinigame_Repeat:
                    step = Sequence.Storyboard05;
                    break;
                case Sequence.PlantingMinigame:
                    step = Sequence.Storyboard06;
                    break;
            }

            currentPage.anim.Play("Outro");
            ActionAfterTransition(AdvanceSequence, currentPage.anim);
            GameManager.instance.ChangeState(GameManager.GameStatus.MINIGAME_END);
        }

        private void ActivateInteraction()
        {
            ChangeHintState("TapZone", true);
        }
    }
}
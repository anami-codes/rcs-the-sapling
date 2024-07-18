using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainbowCat.TheSapling.Minigames;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class MinigameManager : PageManager
    {
        [Header("Minigame Params")]
        public Minigame.Type minigameType = Minigame.Type.None;
        public MinigameTarget[] targets;
        public GaugeBar gaugeBar;

        protected override void Initialize(ChapterManager chapter)
        {
            page = new Page(id, chapter, this, minigameType);
        }

        public void PlayVictorySound()
        {
            SoundManager.instance.PlaySound("sfx_mg_victory");
        }

        public override void BeginPage()
        {
            base.BeginPage();
            gaugeBar?.Initialize(this);
        }

        protected override void Clear()
        {
            base.Clear();
        }
    }
}
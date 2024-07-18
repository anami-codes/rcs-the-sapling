using RainbowCat.TheSapling.InternalStructure;
using RainbowCat.TheSapling.Interactables;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigames
{
    public class HoldTarget : MinigameTarget
    {
        [Header("Hold")]
        public string triggerID;
        public TargetStages[] stages;

        public override void Initialize(Minigame minigame)
        {
            base.Initialize(minigame);
            m_sprite = GetComponentInChildren<SpriteRenderer>();
            m_sprite.color = stages[m_stage].color;
            m_targetProgress = 0.0f;
        }

        public override void GameUpdate(float delta)
        {
            base.GameUpdate(delta);

            if (!isReady && m_isActive)
                UpdateTimer(delta);
        }

        public override void SetOffTrigger(string triggerID, bool isActive, Interactable other)
        {
            if (!isReady && this.triggerID == triggerID)
            {
                m_isActive = isActive;
                if (m_isActive && !m_inLastStage) StartStage();
            }
        }

        public override TargetStatus GetStatus()
        {
            float maxValue = (stages.Length - 1.0f);
            float currentValue = (m_targetProgress > maxValue) ? maxValue : m_targetProgress;
            return new TargetStatus(currentValue, maxValue);
        }

        private void StartStage()
        {
            if (m_timer <= 0.0f) 
                m_timer = stages[m_stage].time;

            float t = m_timer / stages[m_stage].time;
            m_sprite.color = Color.Lerp(stages[m_stage + 1].color, stages[m_stage].color, t);
        }

        private void UpdateTimer(float delta)
        {
            if (m_timer > 0.0f)
            {
                m_timer -= delta;
                float t = m_timer / stages[m_stage].time;
                m_sprite.color = Color.Lerp(stages[m_stage + 1].color, stages[m_stage].color, t);
                m_targetProgress = m_stage + (1.0f - t);
            }

            if (m_timer <= 0.0f)
            {
                m_stage++;
                if (m_inLastStage)
                    SetAsReady();
                else
                    StartStage();
            }
        }

        private float m_targetProgress;

        private SpriteRenderer m_sprite;
        private bool m_isActive;
        private bool m_inLastStage
        {
            get
            {
                return (m_stage >= stages.Length - 1);
            }
        }
    }

    [System.Serializable]
    public struct TargetStages
    {
        public string name;
        public float time;
        public Color color;
        public string animation;
    }
}
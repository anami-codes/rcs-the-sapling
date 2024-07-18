using UnityEngine;
using UnityEngine.UI;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Minigames
{
    public class GaugeBar : MonoBehaviour
    {
        public Material gaugeMat;
        public RectTransform gaugeObj;
        public RectTransform imgMarker;

        public GaugeBarInfo[] gaugeBarInfos;

        public void Initialize(MinigameManager minigame)
        {
            this.minigame = minigame;

            for(int i = 0; i < gaugeBarInfos.Length; i++)
            {
                if (gaugeBarInfos[i].minigameId == minigame.id)
                {
                    gaugeMat.SetColor("_FillColor", gaugeBarInfos[i].fillColor);
                    gaugeMat.SetColor("_BackgroundColor", gaugeBarInfos[i].backgroundColor);
                    imgMarker.GetComponent<Image>().sprite = gaugeBarInfos[i].markerImage;
                }
            }

            markerMaxPos = gaugeObj.rect.width;
            UpdateGauge(0.0f);
        }

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public float CalculateProgress()
        {
            float totalCurrentValue = 0.0f;
            float totalMaxValue = 0.0f;

            foreach(MinigameTarget target in minigame.targets)
            {
                if(target.isNecessary)
                {
                    TargetStatus status = target.GetStatus();
                    totalCurrentValue += status.currentValue;
                    totalMaxValue += status.maxValue;
                }
            }

            if (totalMaxValue > 0.0f)
                return (totalCurrentValue / totalMaxValue);
            else
                return 0.0f;
        }

        private void UpdateGauge(float newValue)
        {
            if (newValue < 0.0f)
                barProgress = 0.0f;
            else if (newValue > 1.0f)
                barProgress = 1.0f;
            else
                barProgress = newValue;

            gaugeMat.SetFloat("_BarProgress", barProgress);
            Vector3 pos = imgMarker.anchoredPosition;
            pos.x = (barProgress * markerMaxPos);
            imgMarker.anchoredPosition = pos;
        }

        private void Update()
        {
            UpdateGauge( CalculateProgress() );
        }

        private MinigameManager minigame;
        private float barProgress = 0.0f;
        private float markerMaxPos = 0.0f;
    }

    [System.Serializable]
    public struct GaugeBarInfo
    {
        public string minigameId;
        public Color fillColor;
        public Color backgroundColor;
        public Sprite markerImage;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Minigames
{
    public class CollectibleGoal : MonoBehaviour
    {
        public List<SpriteRenderer> sprites;

        private void Awake()
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.gameObject.SetActive(false);
            }
            m_currentIndex = 0;
        }

        public void AddSprite(Sprite sprite)
        {
            sprites[m_currentIndex].sprite = sprite;
            sprites[m_currentIndex].gameObject.SetActive(true);
            m_currentIndex++;
        }

        private int m_currentIndex;
    }
}
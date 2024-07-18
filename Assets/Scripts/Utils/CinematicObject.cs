using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Utils
{
    public class CinematicObject : MonoBehaviour
    {
        public string id;
        public string groupID;

        public SpriteRenderer lineArt;
        public SpriteRenderer color;

        public void Paint(bool erase)
        {
            m_erase = erase;
            m_begin = (erase) ? 1.0f : 0.0f;
            m_target = (erase) ? 0.0f : 1.0f;
            Paint(lineArt, 1.0f);
            Paint(color, 1.0f);

            m_lineArtTimer = waitTime;
            m_colorTimer = waitTime;

            if (!erase)
                gameObject.SetActive(true);
        }

        public void GameUpdate(float delta)
        {
            if(m_lineArtTimer > 0.0f)
            {
                m_lineArtTimer -= delta;
                float t = m_lineArtTimer / waitTime;
                Paint(lineArt, t);
            }

            if((m_lineArtTimer <= waitTime - delay) && (m_colorTimer > 0.0f))
            {
                m_colorTimer -= delta;
                float t = m_colorTimer / waitTime;
                Paint(color, t);
                if (m_erase && m_colorTimer <= 0.0f)
                    gameObject.SetActive(false);
            }
            
        }

        private void Paint(SpriteRenderer sprite, float t)
        {
            Color c = sprite.color;
            c.a = Mathf.Lerp(m_target, m_begin, t);
            sprite.color = c;
        }

        private const float waitTime = 0.75f;
        private const float delay = 0f;

        private float m_lineArtTimer;
        private float m_colorTimer;
        private float m_begin;
        private float m_target;

        private bool m_erase;
    }
}
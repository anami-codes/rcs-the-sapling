using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.Utils
{
    public class SoundEffect : MonoBehaviour
    {
        public void Play(AudioClip clip, float volume)
        {
            AudioSource src = GetComponent<AudioSource>();
            src.clip = clip;
            src.volume = volume;
            src.Play();

            Destroy(gameObject, 5.0f);
        }
    }
}
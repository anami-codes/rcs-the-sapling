using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainbowCat.TheSapling;
using RainbowCat.TheSapling.Utils;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get { return m_instance; } }

    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;
    public GameObject sxfPrefab;

    public int defaultBGM = 0;
    public float fadeTime = 5.0f;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        m_instance = this;
        musicSource.volume = 0.0f;
        currentBGM = defaultBGM;
        ChangeBGM(true);
        changingBGM = true;
    }

    void Update()
    {
        if(changingBGM)
        {
            t += (Time.deltaTime / fadeTime);
            float min = (decreaseVolume) ? Game.settings.bgmVolume : 0.0f;
            float max = (decreaseVolume) ? 0.0f : Game.settings.bgmVolume;

            musicSource.volume = Mathf.Lerp(min, max, t);

            if(t >= 1.0f)
            {
                ChangeBGM();
            }
        }
    }

    public void UpdateSound()
    {
        musicSource.volume = Game.settings.bgmVolume;
    }

    public void PlayMusic(int index)
    {
        if (currentBGM == index) return;

        currentBGM = index;
        decreaseVolume = true;
        changingBGM = true;
    }

    public void PlaySound (string sfxId)
    {
        AudioClip clip = GetSFX(sfxId);
        if (clip != null)
        {
            GameObject sfx = Instantiate(sxfPrefab);
            sfx.GetComponent<SoundEffect>().Play(clip, Game.settings.sfxVolume);
        }
    }

    public void ChangeBGM(bool force = false)
    {
        if (decreaseVolume || force)
        {
            musicSource.clip = musicClips[currentBGM];
            musicSource.Play();
            decreaseVolume = false;
        }
        else
        {
            changingBGM = false;
        }

        t = 0.0f;
    }

    private AudioClip GetSFX(string id)
    {
        foreach(AudioClip clip in sfxClips)
        {
            if (clip.name == id)
                return clip;
        }

        if (!(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)))
            Debug.LogWarning("Couldn't find sfx " + id);

        return null;
    }

    private bool changingBGM = false;
    private bool decreaseVolume = false;
    private int currentBGM;
    private float t;

    private AudioSource musicSource;

    private static SoundManager m_instance;
}

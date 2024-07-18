using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RainbowCat.TheSapling.InternalStructure;

namespace RainbowCat.TheSapling.Utils
{
    public class SettingsHelper : MonoBehaviour
    {
        public TMP_Dropdown screenResolution;
        public Toggle fullscreen;
        public Slider bgmVolume;
        public Slider sfxVolume;

        public void SetInitialValues()
        {
            List<string> screenResolutions = new List<string>();
            int index = 0;

            int total = Game.settings.totalAvailableResolutions;
            for (int i = 0; i < total; i++)
            {
                string resName = Game.settings.GetResolution(i).name;
                screenResolutions.Add(resName);
                if (resName == Game.settings.screenResolution.name)
                    index = i;
            }

            if (screenResolution != null && fullscreen != null)
            {
                screenResolution.AddOptions(screenResolutions);
                screenResolution.SetValueWithoutNotify(index);
                fullscreen.isOn = Game.settings.fullscreen;
            }

            bgmVolume.value = Game.settings.bgmVolume;
            sfxVolume.value = Game.settings.sfxVolume;
        }

        public void ChangeResolution()
        {
            string screenRes = screenResolution.options[screenResolution.value].text;
            bool isFullscreen = fullscreen.isOn;
            Game.settings.ChangeResolution(screenRes, isFullscreen);
        }

        public void ChangeBGMVolume()
        {
            Game.settings.ChangeBGMVolume(bgmVolume.value);
        }

        public void ChangeSFXVolume()
        {
            Game.settings.ChangeSFXVolume(sfxVolume.value);
        }
    }
}
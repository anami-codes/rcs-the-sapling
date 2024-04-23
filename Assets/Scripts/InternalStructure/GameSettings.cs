using System.Collections.Generic;
using UnityEngine;

namespace RainbowCat.TheSapling.InternalStructure
{
    public class GameSettings
    {
        public int totalAvailableResolutions { get { return sizes.Count; } }
        public ScreenResolution screenResolution { get; private set; }
        public bool fullscreen { get; private set; }

        public GameSettings()
        {
            LoadResolutions();
            fullscreen = Screen.fullScreen;
            screenResolution = new ScreenResolution(Screen.width, Screen.height);
        }

        public void ChangeResolution(string resName, bool fullscreen)
        {
            screenResolution = GetResolution(resName);
            this.fullscreen = fullscreen;
            Screen.SetResolution(screenResolution.x, screenResolution.y, this.fullscreen);
        }

        public ScreenResolution GetResolution (int index)
        {
            return sizes[index];
        }

        private ScreenResolution GetResolution (string resName)
        {
            foreach(ScreenResolution screenRes in sizes)
            {
                if (screenRes.name == resName)
                    return screenRes;
            }
            return new ScreenResolution(1280, 720);
        }

        private void LoadResolutions()
        {
            sizes.Add(new ScreenResolution(640, 480));
            sizes.Add(new ScreenResolution(720, 480));
            sizes.Add(new ScreenResolution(720, 576));
            sizes.Add(new ScreenResolution(800, 600));
            sizes.Add(new ScreenResolution(1024, 768));
            sizes.Add(new ScreenResolution(1152, 864));
            sizes.Add(new ScreenResolution(1176, 664));
            sizes.Add(new ScreenResolution(1280, 720));
            sizes.Add(new ScreenResolution(1280, 768));
            sizes.Add(new ScreenResolution(1280, 800));
            sizes.Add(new ScreenResolution(1280, 960));
            sizes.Add(new ScreenResolution(1280, 1024));
            sizes.Add(new ScreenResolution(1360, 768));
            sizes.Add(new ScreenResolution(1360, 850));
            sizes.Add(new ScreenResolution(1440, 900));
            sizes.Add(new ScreenResolution(1440, 1080));
            sizes.Add(new ScreenResolution(1600, 900));
            sizes.Add(new ScreenResolution(1600, 1024));
            sizes.Add(new ScreenResolution(1600, 1050));
            sizes.Add(new ScreenResolution(1920, 1080));
        }

        private List<ScreenResolution> sizes = new List<ScreenResolution>();
    }

    public struct ScreenResolution
    {
        public string name 
        {
            get
            {
                return x + " x " + y;
            }
        }
        public int x { get; private set; }
        public int y { get; private set; }

        public ScreenResolution (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
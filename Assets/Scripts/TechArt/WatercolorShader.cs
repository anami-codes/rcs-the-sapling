using RainbowCat.TheSapling.InternalStructure;
using UnityEngine;

namespace RainbowCat.TheSapling.TechArt
{
    public static class WatercolorShader
    {
        public static void Initialize()
        {
            //Load materials from JSON
            Reset();
        }

        public static void Reset()
        {
            float r = CalculateRadius(1.0f);
            float hardness = CalculateHardness(1.0f);
            for (int i = 0; i < Game.manager.gameMaterials.Length; i++)
            {
                Game.manager.gameMaterials[i].SetFloat("_Radius", r);
                Game.manager.gameMaterials[i].SetFloat("_Hardness", hardness);
                Game.manager.gameMaterials[i].SetFloat("_Progress", 1.0f);
            }
        }

        public static void Paint (float t)
        {
            float radius = CalculateRadius(t);
            float hardness = CalculateHardness(t);

            for (int i = 0; i < Game.manager.gameMaterials.Length; i++)
            {
                Game.manager.gameMaterials[i].SetFloat("_Radius", radius);
                Game.manager.gameMaterials[i].SetFloat("_Hardness", hardness);
                Game.manager.gameMaterials[i].SetFloat("_Progress", t);
            }
        }

        private static float CalculateRadius(float t)
        {
            float radius = 0.0f;
            float size = 5.0f;
            if (Game.cameraController != null)
                size = CameraController.gameCamera.orthographicSize;

            if (t >= 0.0f)
                radius = Mathf.Sqrt(t) * (size * 2f);

            return radius;
        }

        private static float CalculateHardness(float t)
        {
            float minHardness = 1f;
            float maxHardness = -0.5f;
            
            float minT = 0.0f;
            float maxT = 0.3f;
            float fixedT = (t - minT) / (maxT - minT);

            float value = minHardness;

            if (t >= minT && t <= maxT)
                value = minHardness + ( (maxHardness - minHardness) * fixedT);
            else if (t > maxT)
                value = maxHardness;

            return value;
        }

        private static Material[] materials;
    }
}
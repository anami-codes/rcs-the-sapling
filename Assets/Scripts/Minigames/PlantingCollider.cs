using UnityEngine;
using RainbowCat.TheSapling.Interactables;
using RainbowCat.TheSapling.Minigames;

public class PlantingCollider : InteractableObject
{
    public PlantingMinigame plantingMinigame;
    public int index;
    public bool isReady = false;

    public HintControl extraHint;

    public void ZoomIn()
    {
        plantingMinigame.ZoomIn(index - 1);
        m_timer = 1.5f;
    }

    private void Update()
    {
        if(m_timer > 0.0f)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0.0f)
                extraHint.StartHint();
        }
    }

    float m_timer = 0.0f;
}

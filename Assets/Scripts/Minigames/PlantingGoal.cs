using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainbowCat.TheSapling.Minigames;
using RainbowCat.TheSapling.Interactables;

public class PlantingGoal : InteractableObject
{
    public PottedPlant target;
    public SpriteRenderer sprite;
    public int digTimes;

    private void Start()
    {
        Vector3 scale = sprite.transform.localScale;
        scale.x = m_currentDig;
        sprite.transform.localScale = scale;
    }

    public void Dig()
    {
        if (m_currentDig >= digTimes) return;

        m_currentDig++;

        Vector3 scale = sprite.transform.localScale;
        scale.x = m_currentDig;
        sprite.transform.localScale = scale;
    }

    public bool CanPlant()
    {
        return (m_currentDig >= digTimes);
    }

    private int m_currentDig = 0;
}
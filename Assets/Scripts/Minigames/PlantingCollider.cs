using RainbowCat.TheSapling.Interactables;
using RainbowCat.TheSapling.Minigames;

public class PlantingCollider : InteractableObject
{
    public PlantingMinigame plantingMinigame;
    public int index;
    public bool isReady = false;

    public void ZoomIn()
    {
        plantingMinigame.ZoomIn(index - 1);
    }
}

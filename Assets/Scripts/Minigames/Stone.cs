using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int lifePoints = 1;

    public void Restart( DiggingMinigame minigame )
    {
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
            this.minigame = minigame;
        }

        currentLife = lifePoints;
        UpdateSize();        
        rect.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 1.0f) * 360.0f );
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }

    void IPointerUpHandler.OnPointerUp( PointerEventData eventData )
    {
        currentLife--;

        if (currentLife > 0 )
        {
            UpdateSize();
        }
        else
        {
            GetComponent<Image>().enabled = false;
            minigame.CheckConditions();
        }
        
    }

    private void UpdateSize()
    {
        float stoneSize = sizeMult + (sizeMult * currentLife);
        rect.sizeDelta = new Vector2(stoneSize, stoneSize);
    }

    public int life { get { return currentLife; } }

    private DiggingMinigame minigame;
    private RectTransform rect;
    private int currentLife;
    private float sizeMult = 25.0f;
}

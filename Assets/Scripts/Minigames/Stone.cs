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
            rect.sizeDelta = new Vector2( 50.0f, 50.0f ) * lifePoints;
            this.minigame = minigame;
        }

        currentLife = lifePoints;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }

    void IPointerUpHandler.OnPointerUp( PointerEventData eventData )
    {
        currentLife--;

        if (currentLife > 0 )
        {
            rect.sizeDelta = new Vector2( 50.0f, 50.0f ) * currentLife;
        }
        else
        {
            GetComponent<Image>().enabled = false;
            minigame.CheckConditions();
        }
        
    }

    public int life { get { return currentLife; } }

    private DiggingMinigame minigame;
    private RectTransform rect;
    private int currentLife;
}

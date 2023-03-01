using UnityEngine;
using UnityEngine.EventSystems;


public class Cloud : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void RestartPosition( CloudsMinigame minigame )
    {
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
            initialPos = rect.position;
            this.minigame = minigame;
        }

        rect.position = initialPos;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 diff = eventData.position - clickPos;
        rect.position = startPos + diff;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        startPos = rect.position;
        clickPos = eventData.pressPosition;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        minigame.CheckConditions();
    }

    private CloudsMinigame minigame;

    private RectTransform rect;
    private Vector2 initialPos;

    private Vector2 startPos;
    private Vector2 clickPos;
}

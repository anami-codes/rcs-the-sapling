using UnityEngine;
using RainbowCat.TheSapling;

public class CreditsScroller : MonoBehaviour
{
    public RectTransform objTransform;
    public RectTransform creditsEnd;
    public RectTransform marker;

    public float speed = 1.0f;

    void OnEnable()
    {
        objTransform.anchoredPosition = new Vector3(0, -1100, 0);
        canScroll = true;
    }

    void Update()
    {
        if(gameObject.activeSelf && canScroll)
        {
            Vector3 newPos = objTransform.position;
            newPos.y += Time.deltaTime * speed;
            objTransform.position = newPos;

            if (MarkerInPosition())
            {
                canScroll = false;
                waitingEnd = true;
            }
        }

        if(waitingEnd)
        {
            endTimer -= Time.deltaTime;
            if(endTimer <= 0.0f)
            {
                waitingEnd = false;
                Game.SetOffTrigger("EndChapter");
            }
        }
    }

    private bool MarkerInPosition()
    {
        return (creditsEnd.position.y >= marker.position.y);
    }

    private bool canScroll = false;
    private bool waitingEnd = false;
    private float endTimer = 4.0f;
}

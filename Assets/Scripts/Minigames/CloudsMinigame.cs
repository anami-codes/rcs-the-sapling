using UnityEngine;

public class CloudsMinigame : BaseMinigame
{
    public RectTransform gameArea;
    public RectTransform sunArea;
    public RectTransform cloudContainer;

    protected override void InitializeMinigame(GameManager gameManager)
    {
        base.InitializeMinigame(gameManager);
        clouds = cloudContainer.GetComponentsInChildren<Cloud>();
        minPos = gameArea.Find("MinPos").GetComponent<RectTransform>().position;
        maxPos = gameArea.Find("MaxPos").GetComponent<RectTransform>().position;
    }

    public override void StartMinigame( GameManager gameManager )
    {
        if ( !this.gameManager )
            InitializeMinigame(gameManager);

        for ( int i = 0; i < clouds.Length; i++)
        {
            clouds[i].RestartPosition( this );
        }

        base.StartMinigame(gameManager);
    }

    public override void CheckConditions()
    {
        if (inGame)
        {
            for (int i = 0; i < clouds.Length; i++)
            {
                Vector2 pos = clouds[i].GetComponent<RectTransform>().position;
                if ((minPos.x < pos.x && pos.x < maxPos.x) &&
                    (minPos.y < pos.y && pos.y < maxPos.y))
                {
                    return;
                }
            }
        }

        inGame = false;
        anim.SetBool("Show", false);
    }

    protected override void EndMinigame()
    {
        base.EndMinigame();
    }

    private Cloud[] clouds;
    private Vector2 minPos;
    private Vector2 maxPos;
}

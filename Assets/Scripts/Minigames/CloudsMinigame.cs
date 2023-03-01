using UnityEngine;

public class CloudsMinigame : BaseMinigame
{
    public RectTransform gameArea;
    public RectTransform sunArea;
    public RectTransform cloudContainer;

    public override void StartMinigame( GameManager gameManager )
    {
        if ( !this.gameManager )
        {
            base.StartMinigame( gameManager );
            clouds = cloudContainer.GetComponentsInChildren<Cloud>();
            minPos = gameArea.Find("MinPos").GetComponent<RectTransform>().position;
            maxPos = gameArea.Find("MaxPos").GetComponent<RectTransform>().position;
        }

        for ( int i = 0; i < clouds.Length; i++)
        {
            clouds[i].RestartPosition( this );
        }
    }

    public override void CheckConditions()
    {
        base.CheckConditions();
        for (int i = 0; i < clouds.Length; i++)
        {
            Vector2 pos = clouds[i].GetComponent<RectTransform>().position;
            if ( ( minPos.x < pos.x && pos.x < maxPos.x ) && 
                ( minPos.y < pos.y && pos.y < maxPos.y ) )
            {
                return;
            }
        }
        EndMinigame();
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
    }

    private Cloud[] clouds;
    private Vector2 minPos;
    private Vector2 maxPos;
}

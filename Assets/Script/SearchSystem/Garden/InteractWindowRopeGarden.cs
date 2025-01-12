using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWindowRopeGarden : MonoBehaviour, IReceiveSearch
{
    //2ŠKŠJ•úŒãƒ[ƒv•t‚«‘‹

    [SerializeField] private DialogueText F2MoveText; //2ŠK‚Ö‚ÌˆÚ“®
    [SerializeField] private DialogueText MoveAfterText;//2ŠKˆÚ“®Œã

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò
        //2ŠK‚Ö‚ÌˆÚ“®‘O
        if (!gameManager.eventFlagData.F2Intrusion)
        {
            gameManager.OpenTextWindow(F2MoveText);
        }
        //2ŠK‚Ö‚ÌˆÚ“®Œã
        else
        {
            gameManager.OpenTextWindow(MoveAfterText);
        }
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {

    }
}

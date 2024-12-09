using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLackyClassRoom : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntroAfterText;
    [SerializeField] private DialogueText GameStartText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        Debug.Log("K‰^‰ï˜b");
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò
        if (gameManager.eventFlagData.GameStart_All)
        {
            gameManager.OpenTextWindow(GameStartText);
        }
        else
        {
            gameManager.OpenTextWindow(SelfIntroAfterText);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfLibrary : MonoBehaviour, IReceiveSearch
{
    //}‘º‚Ì–{’I

    [SerializeField] private DialogueText NormalText; //}‘º‚Ì–{’I
    [SerializeField] private DialogueText AfterText; //}‘º‚Ì–{’I’²¸Œã

    private GameManager gameManager;

    bool Search = false;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò
        
        //
        if (gameManager.eventFlagData.F2Request && !Search)
        {
            Search = true;
            gameManager.OpenTextWindow(NormalText);
        }
        else
        {
            gameManager.OpenTextWindow(AfterText);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArcherySet : MonoBehaviour, IReceiveSearch
{
    //‹|“¹‹ïˆê®‚Ì“üè

    [SerializeField] private DialogueText GetText;
    [SerializeField] private DialogueText GetAfterText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(GetText);
        }
        else
        {
            gameManager.OpenTextWindow(GetAfterText);
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

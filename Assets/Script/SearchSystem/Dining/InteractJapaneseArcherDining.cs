using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractJapaneseArcherDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntoroText;
    [SerializeField] private DialogueText SelfIntoroAfterText;
    [SerializeField] private DialogueText RequestText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò
        //’TõŠJnŒã‚É“SŠiq‚ğ’²‚×‹|“¹‹ïˆê®‚ğ“üè‚µ‚½
        if (!gameManager.eventFlagData.DiscStart && gameManager.eventFlagData.IronBars && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(RequestText);
        }
        //©ŒÈĞ‰î
        else if (!gameManager.eventFlagData.SelfIntoro_Archer)
        {
            gameManager.OpenTextWindow(SelfIntoroText);
        }
        //©ŒÈĞ‰îI—¹Œã
        else
        {
            gameManager.OpenTextWindow(SelfIntoroAfterText);
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

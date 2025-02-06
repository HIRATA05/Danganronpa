using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractChestMachineWorkRoom : MonoBehaviour, IReceiveSearch
{
    //‹@ŠBHìº‚Ì’I

    [SerializeField] private DialogueText NormalText;//’I‚ÌŒ®Šm”F
    [SerializeField] private DialogueText KeyUnlockText;//’I‚ÌŒ®‰ğœ‚ÅŒ®“üè
    [SerializeField] private DialogueText AfterText;//Œ®“üèŒã

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //ê‡‚É‚æ‚Á‚Ä‚Íƒtƒ‰ƒO‚É‚æ‚Á‚ÄğŒ•ªŠò

        //’I‚ÌŒ®‰ğœ
        if (gameManager.eventFlagData.PressMachineShelfUnlock && gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag == true)
        {
            gameManager.OpenTextWindow(AfterText);
        }
        else if (gameManager.eventFlagData.PressMachineShelfUnlock && gameManager.eventFlagData.PreesMoveDown)
        {
            gameManager.eventFlagData.PressMachineShelfUnlock = true;
            gameManager.OpenTextWindow(KeyUnlockText);
        }
        else
        {
            gameManager.OpenTextWindow(NormalText);
        }
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }
}

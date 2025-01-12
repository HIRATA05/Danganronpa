using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLightGarden : MonoBehaviour, IReceiveSearch
{
    //ƒ‰ƒCƒg

    [SerializeField] private DialogueText GetText; //“d’r“όθ
    [SerializeField] private DialogueText AfterText;//“όθγ

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //κ‡‚Ι‚ζ‚Α‚Δ‚Νƒtƒ‰ƒO‚Ι‚ζ‚Α‚Δπ•ªς
        //ηκ’ά‚π“όθ‚µ‚Δ‚Ά‚Θ‚Ά“όθ
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(GetText);
        }
        //’Tυγ
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

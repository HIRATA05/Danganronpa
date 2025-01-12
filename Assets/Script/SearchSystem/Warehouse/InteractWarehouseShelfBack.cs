using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWarehouseShelfBack : MonoBehaviour, IReceiveSearch
{
    //‘qΙ‰‚Μ’I

    [SerializeField] private DialogueText ClawGetText; //ηκ’ά“όθ
    [SerializeField] private DialogueText NormalText;//’Κν

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //κ‡‚Ι‚ζ‚Α‚Δ‚Νƒtƒ‰ƒO‚Ι‚ζ‚Α‚Δπ•ªς
        //ηκ’ά‚π“όθ‚µ‚Δ‚Ά‚Θ‚Ά“όθ
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag)
        {
            gameManager.OpenTextWindow(ClawGetText);
        }
        //’Tυγ
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

    void Update()
    {

    }
}

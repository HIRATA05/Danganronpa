using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWarehouseShelf : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText WrenchGetText; //レンチ入手
    [SerializeField] private DialogueText NormalText;//通常

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //レンチを入手していない時入手
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[4].getFlag)
        {
            gameManager.OpenTextWindow(WrenchGetText);
        }
        //探索後
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

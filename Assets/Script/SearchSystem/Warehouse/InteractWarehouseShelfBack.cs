using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWarehouseShelfBack : MonoBehaviour, IReceiveSearch
{
    //倉庫奥の棚

    [SerializeField] private DialogueText ClawGetText; //鉤爪入手
    [SerializeField] private DialogueText NormalText;//通常

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //鉤爪を入手していない時入手
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag)
        {
            gameManager.OpenTextWindow(ClawGetText);
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

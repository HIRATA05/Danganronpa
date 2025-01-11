using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class InteractJapaneseArcherDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText SelfIntoroText;
    [SerializeField] private DialogueText SelfIntoroAfterText;
    [SerializeField] private DialogueText RequestText;
    [SerializeField] private DialogueText RequestAfterText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //探索開始後に鉄格子を調べ弓道具一式を入手した
        if (!gameManager.eventFlagData.WarehouseRequest && gameManager.eventFlagData.IronBars && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(RequestText);
        }
        //倉庫の仕掛け解除要請後
        else if (gameManager.eventFlagData.WarehouseRequest)
        {
            gameManager.OpenTextWindow(RequestAfterText);
        }
        //自己紹介
        else if (!gameManager.eventFlagData.SelfIntoro_Archer)
        {
            gameManager.OpenTextWindow(SelfIntoroText);
        }
        //自己紹介終了後
        else
        {
            gameManager.OpenTextWindow(SelfIntoroAfterText);
        }

    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();

        //部屋に入った時の表示判定
        if (gameManager.eventFlagData.WarehouseRequest)
        {
            //倉庫の仕掛け解除要請後は部屋から消える
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }

    void Update()
    {

    }
}

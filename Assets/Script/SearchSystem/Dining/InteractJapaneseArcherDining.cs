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
        //場合によってはフラグによって条件分岐
        //探索開始後に鉄格子を調べ弓道具一式を入手した
        if (!gameManager.eventFlagData.DiscStart && gameManager.eventFlagData.IronBars && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag)
        {
            gameManager.OpenTextWindow(RequestText);
        }
        //自己紹介
        else if (!gameManager.eventFlagData.SelfIntoro_Acher)
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
    }

    void Update()
    {

    }
}

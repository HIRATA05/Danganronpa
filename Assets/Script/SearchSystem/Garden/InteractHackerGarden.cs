using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHackerGarden : MonoBehaviour, IReceiveSearch
{
    //ハッカー自己紹介

    [SerializeField] private DialogueText SelfIntoroText; //自己紹介
    [SerializeField] private DialogueText NormalText;//通常

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //鉤爪を入手していない時入手
        if (!gameManager.eventFlagData.SelfIntoro_Hacker)
        {
            gameManager.OpenTextWindow(SelfIntoroText);
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

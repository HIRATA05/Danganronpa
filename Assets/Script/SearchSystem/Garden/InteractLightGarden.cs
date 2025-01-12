using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLightGarden : MonoBehaviour, IReceiveSearch
{
    //ライト

    [SerializeField] private DialogueText GetText; //電池入手
    [SerializeField] private DialogueText AfterText;//入手後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //鉤爪を入手していない時入手
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(GetText);
        }
        //探索後
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

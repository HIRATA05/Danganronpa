using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class InteractPhantomThiefGarden : MonoBehaviour, IReceiveSearch
{
    //中庭で怪盗との会話

    [SerializeField] private DialogueText RequestText; //2階への移動を提案
    [SerializeField] private DialogueText RequestClaerBeforText; //2階への移動を提案後アイテム無し
    [SerializeField] private DialogueText RequestClaerAfterText; //2階への移動を提案後アイテム有り
    [SerializeField] private DialogueText NormalText;//2階解放後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //2階解放後
        if (gameManager.eventFlagData.RopeWindow)
        {
            gameManager.OpenTextWindow(NormalText);
        }
        //2階への移動を提案後アイテム有り　ロープ、鉤爪、弓道具セット入手後125
        else if(gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag && gameManager.eventFlagData.itemDataBase.truthBullets[2].getFlag &&
            gameManager.eventFlagData.itemDataBase.truthBullets[5].getFlag && gameManager.eventFlagData.F2Open)
        {
            gameManager.OpenTextWindow(RequestClaerAfterText);
        }
        //2階への移動を提案後アイテム無し
        else if (gameManager.eventFlagData.F2Request)
        {
            gameManager.OpenTextWindow(RequestClaerAfterText);
        }
        //2階への移動を提案
        else
        {
            gameManager.OpenTextWindow(RequestText);
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

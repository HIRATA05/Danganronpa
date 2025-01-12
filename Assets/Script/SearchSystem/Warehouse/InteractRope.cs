using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRope : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText RopeGetText; //ロープ入手
    [SerializeField] private DialogueText NormalText;//通常

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //レンチを入手していない時入手
        if (!gameManager.eventFlagData.itemDataBase.truthBullets[1].getFlag)
        {
            gameManager.OpenTextWindow(RopeGetText);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPhantomThiefEntranceHall : MonoBehaviour, IReceiveSearch
{
    //玄関ホールの

    [SerializeField] private DialogueText NormalText;

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐

        gameManager.OpenTextWindow(NormalText);
    }

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //探索開始前や脱出時表示する
        if (!gameManager.eventFlagData.AdventureStart)
        {
            gameObject.SetActive(true);
        }
        if (gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag)
        {
            gameObject.SetActive(true);
        }
    }
}

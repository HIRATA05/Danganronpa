using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClockDining : MonoBehaviour, IReceiveSearch
{
    [SerializeField] private DialogueText DigitalClockNoBatteryText; //デジタル時計電池入手前
    [SerializeField] private DialogueText DigitalClockBatteryInText;//デジタル時計電池入手後
    [SerializeField] private DialogueText DigitalClockInAfter;//デジタル時計電池装填後

    private GameManager gameManager;

    public void ReceiveSearch()
    {
        //場合によってはフラグによって条件分岐
        //電池を入れた後
        if (gameManager.eventFlagData.Digitalclock)
        {
            gameManager.OpenTextWindow(DigitalClockInAfter);
        }
        //電池を持っている時
        else if(gameManager.eventFlagData.itemDataBase.truthBullets[3].getFlag)
        {
            gameManager.OpenTextWindow(DigitalClockBatteryInText);
        }
        //電池を持っていない時
        else
        {
            gameManager.OpenTextWindow(DigitalClockNoBatteryText);
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

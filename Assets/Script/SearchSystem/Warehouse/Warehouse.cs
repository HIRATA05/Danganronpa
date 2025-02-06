using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomFirstText;
    [SerializeField] private DialogueText RequestText;

    private GameManager gameManager;
    private EventFlagData eventFlagData;

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        eventFlagData = gameManager.eventFlagData;
    }

    void Update()
    {
        if (gameManager.playerController == GameManager.PlayerController.ReticleMode)
        {
            //始めて倉庫へ入った時の会話
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_Warehouse)
            {
                eventFlagData.RoomIn_Warehouse = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //扉開錠の要請
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.WarehouseRequest && eventFlagData.IronBars && !eventFlagData.IronBarsOpenBefor)
            {
                eventFlagData.IronBarsOpenBefor = true;
                gameManager.OpenTextWindow(RequestText);
            }
        }

    }
}

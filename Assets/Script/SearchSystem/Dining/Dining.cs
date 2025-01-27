using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;
    [SerializeField] private DialogueText DiscStartText;
    [SerializeField] private DialogueText MonokumaCallText;

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
            //始めて食堂へ入った時の会話
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_Dining)
            {
                Debug.Log("RoomIn_Dining");
                eventFlagData.RoomIn_Dining = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //モノクマの悪ふざけの会話
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.AdventureStart && eventFlagData.IronBars && eventFlagData.WarehouseRequest && !eventFlagData.DiningDiscStart)
            {
                Debug.Log("WarehouseRequest");
                eventFlagData.DiningDiscStart = true;
                gameManager.OpenTextWindow(DiscStartText);
            }
            //自己紹介後呼び出し
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.SelfIntoro_All && !eventFlagData.SelfIntoro_Call)
            {
                Debug.Log("モノクマの登場");
                eventFlagData.SelfIntoro_Call = true;
                gameManager.OpenTextWindow(MonokumaCallText);
            }
        }

    }
}

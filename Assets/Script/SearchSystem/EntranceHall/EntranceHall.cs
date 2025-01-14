using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class EntranceHall : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomFirstText;
    [SerializeField] private DialogueText EscepeText;
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
            //始めて玄関ホールへ入った時の会話
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_EntranceHall)
            {
                eventFlagData.RoomIn_EntranceHall = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //自己紹介後呼び出し
            if (eventFlagData.currentRoom == roomName && eventFlagData.SelfIntoro_All && !eventFlagData.SelfIntoro_Call)
            {
                Debug.Log("モノクマの登場");
                eventFlagData.SelfIntoro_Call = true;
                gameManager.OpenTextWindow(MonokumaCallText);
            }
            //脱出スイッチ入手後
            if (gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag && !gameManager.eventFlagData.EscepeEvent)
            {
                gameManager.eventFlagData.EscepeEvent = true;
                gameManager.OpenTextWindow(EscepeText);
            }

        }
    }
}

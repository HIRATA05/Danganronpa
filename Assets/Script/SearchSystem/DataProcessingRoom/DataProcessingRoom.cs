using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessingRoom : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;
    [SerializeField] private DialogueText MonokumaTruthText;
    [SerializeField] private DialogueText EndText;

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
            //情報処理室へ入った時の会話
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_DataProcessingRoom)
            {
                eventFlagData.RoomIn_DataProcessingRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //探索後モノクマ登場
            else if(/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.DataProcessingRoom_All && !eventFlagData.DataProcessingRoom_AllTalk)
            {
                eventFlagData.DataProcessingRoom_AllTalk = true;
                gameManager.OpenTextWindow(MonokumaTruthText);
            }
            //モノクマとの会話　真実
            else if(/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.AllTalkEndFlag && !eventFlagData.MonokumaTruth_DataProcessingRoom)
            {
                eventFlagData.MonokumaTruth_DataProcessingRoom = true;
                gameManager.OpenTextWindow(EndText);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoom_F2 : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;
    [SerializeField] private DialogueText F2LackyText;

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
            //始めて2階教室へ入った時の会話
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_ClassRoom_F2)
            {
                eventFlagData.RoomIn_ClassRoom_F2 = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //教室探索後
            else if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.ClassRoomF2_All && !eventFlagData.ClassRoomF2_All_TalkStart)
            {
                eventFlagData.ClassRoomF2_All_TalkStart = true;
                gameManager.OpenTextWindow(F2LackyText);
            }
            
        }

    }
}

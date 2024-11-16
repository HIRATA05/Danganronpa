using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //現在いる部屋を定義

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText TestRoomText;

    private GameManager gameManager;


    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //現在の部屋がこの部屋でフラグがTrueで発生フラグが立った時イベント発生
        if (gameManager.eventFlagData.currentRoom == roomName && gameManager.eventFlagData.RoomIn)
        {
            gameManager.OpenTextWindow(TestRoomText);
        }

    }

    //現在の部屋をこの部屋に変更
    public void cureentRoomChange()
    {
        gameManager.eventFlagData.currentRoom = roomName;
    }
}

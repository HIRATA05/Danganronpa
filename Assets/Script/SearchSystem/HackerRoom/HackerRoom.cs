using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerRoom : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;

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
            //ハッカーの個室へ入った時の会話
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_HackerRoom)
            {
                eventFlagData.RoomIn_HackerRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
        }

    }
}

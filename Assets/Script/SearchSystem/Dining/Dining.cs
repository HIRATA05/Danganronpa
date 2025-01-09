using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;
    [SerializeField] private DialogueText DiscStartText;

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

            if (eventFlagData.currentRoom == roomName && eventFlagData.RoomIn_Dining)
            {
                Debug.Log("RoomIn_Dining");
                eventFlagData.RoomIn_Dining = false;
                gameManager.OpenTextWindow(RoomText);
            }
            if (eventFlagData.currentRoom == roomName && eventFlagData.WarehouseRequest)
            {
                Debug.Log("WarehouseRequest");
                eventFlagData.WarehouseRequest = false;
                gameManager.OpenTextWindow(DiscStartText);
            }
        }

    }
}

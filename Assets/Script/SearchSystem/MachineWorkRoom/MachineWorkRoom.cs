using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWorkRoom : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;
    [SerializeField] private DialogueText PreesText;

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
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_MachineWorkRoom)
            {
                eventFlagData.RoomIn_MachineWorkRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //教室探索後
            else if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.HackerPressMachineRequest && !eventFlagData.PreesMoveDown)
            {
                eventFlagData.PreesMoveDown = true;
                gameManager.OpenTextWindow(PreesText);
            }

        }

    }
}

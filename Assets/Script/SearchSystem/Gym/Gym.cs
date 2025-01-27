using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public string roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomFirstText;
    [SerializeField] private DialogueText AppMonokumaAfterText;

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
            //始めて体育館へ入った時の会話
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_Gym)
            {
                eventFlagData.RoomIn_Gym = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //モノクマの話終了後
            else if(eventFlagData.currentRoom == roomName && !eventFlagData.AppMonokumaAfter && eventFlagData.AppMonokumaEnd)
            {
                eventFlagData.AppMonokumaAfter = true;
                gameManager.OpenTextWindow(AppMonokumaAfterText);
            }
        }
    }
}

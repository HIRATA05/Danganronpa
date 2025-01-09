using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class ClassRoom_F1 : MonoBehaviour
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
        if(gameManager.playerController == GameManager.PlayerController.ReticleMode)
        {
            //全て調べ終わった時
            if (eventFlagData.currentRoom == roomName && eventFlagData.GameStart_All)
            {
                Debug.Log("GameStart_All");
                eventFlagData.GameStart_All = false;
                eventFlagData.GameStart_All_TalkStart = true;
                gameManager.OpenTextWindow(RoomText);
            }
        }
        

    }

    
}

using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class ClassRoom_F1 : MonoBehaviour
{
    //Œ»İ‚¢‚é•”‰®‚ğ’è‹` •”‰®‚Ì”‚¾‚¯ì¬

    public string roomName;

    [SerializeField, Header("•”‰®‚Ì’†‚Å‚Ì©“®”­¶‰ï˜b")] private DialogueText RoomText;

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

            if (eventFlagData.currentRoom == roomName && eventFlagData.GameStart_All)
            {
                Debug.Log("GameStart_All");
                eventFlagData.GameStart_All_TalkStart = true;
                gameManager.OpenTextWindow(RoomText);
            }
        }
        

    }

    
}

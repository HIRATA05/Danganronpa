using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;

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
            //�n�߂đ̈�ق֓��������̉�b
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_Library)
            {
                eventFlagData.RoomIn_Library = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }

        }
    }
}

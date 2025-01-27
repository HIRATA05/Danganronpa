using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerRoom : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;

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
            //�n�b�J�[�̌��֓��������̉�b
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_HackerRoom)
            {
                eventFlagData.RoomIn_HackerRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
        }

    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //���݂��镔�����`

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText TestRoomText;

    private GameManager gameManager;


    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //���݂̕��������̕����Ńt���O��True�Ŕ����t���O�����������C�x���g����
        if (gameManager.eventFlagData.currentRoom == roomName && gameManager.eventFlagData.RoomIn)
        {
            gameManager.OpenTextWindow(TestRoomText);
        }

    }

    //���݂̕��������̕����ɕύX
    public void cureentRoomChange()
    {
        gameManager.eventFlagData.currentRoom = roomName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWorkRoom : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;
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
            //�n�߂�2�K�����֓��������̉�b
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_MachineWorkRoom)
            {
                eventFlagData.RoomIn_MachineWorkRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //�����T����
            else if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.HackerPressMachineRequest && !eventFlagData.PreesMoveDown)
            {
                eventFlagData.PreesMoveDown = true;
                gameManager.OpenTextWindow(PreesText);
            }

        }

    }
}

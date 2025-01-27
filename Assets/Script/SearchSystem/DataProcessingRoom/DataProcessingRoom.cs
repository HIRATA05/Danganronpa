using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessingRoom : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;
    [SerializeField] private DialogueText MonokumaTruthText;
    [SerializeField] private DialogueText EndText;

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
            //��񏈗����֓��������̉�b
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_DataProcessingRoom)
            {
                eventFlagData.RoomIn_DataProcessingRoom = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //�T���ヂ�m�N�}�o��
            else if(/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.DataProcessingRoom_All && !eventFlagData.DataProcessingRoom_AllTalk)
            {
                eventFlagData.DataProcessingRoom_AllTalk = true;
                gameManager.OpenTextWindow(MonokumaTruthText);
            }
            //���m�N�}�Ƃ̉�b�@�^��
            else if(/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.AllTalkEndFlag && !eventFlagData.MonokumaTruth_DataProcessingRoom)
            {
                eventFlagData.MonokumaTruth_DataProcessingRoom = true;
                gameManager.OpenTextWindow(EndText);
            }
        }

    }
}

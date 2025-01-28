using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoom_F2 : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;
    [SerializeField] private DialogueText F2LackyText;

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
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_ClassRoom_F2)
            {
                eventFlagData.RoomIn_ClassRoom_F2 = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //�����T����
            else if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.ClassRoomF2_All && !eventFlagData.ClassRoomF2_All_TalkStart)
            {
                eventFlagData.ClassRoomF2_All_TalkStart = true;
                gameManager.OpenTextWindow(F2LackyText);
            }
            
        }

    }
}

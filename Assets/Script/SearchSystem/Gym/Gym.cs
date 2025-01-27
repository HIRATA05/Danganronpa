using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;
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
            //�n�߂đ̈�ق֓��������̉�b
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_Gym)
            {
                eventFlagData.RoomIn_Gym = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //���m�N�}�̘b�I����
            else if(eventFlagData.currentRoom == roomName && !eventFlagData.AppMonokumaAfter && eventFlagData.AppMonokumaEnd)
            {
                eventFlagData.AppMonokumaAfter = true;
                gameManager.OpenTextWindow(AppMonokumaAfterText);
            }
        }
    }
}

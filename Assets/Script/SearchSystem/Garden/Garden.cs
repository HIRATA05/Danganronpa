using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;
    //[SerializeField] private DialogueText OpenText;
    [SerializeField] private DialogueText MonokumaCallText;

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
            //�n�߂Ē���֓��������̉�b
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_Garden)
            {
                eventFlagData.RoomIn_Garden = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            /*
            //2�K�J���C�x���g�@2�K�J����False�ŊJ���\��True�̎�����
            else if (eventFlagData.currentRoom == roomName && !eventFlagData.F2Request && eventFlagData.F2Open)
            {
                eventFlagData.F2Request = true;
                gameManager.OpenTextWindow(OpenText);
            }
            */
            //���ȏЉ��Ăяo��
            if (eventFlagData.currentRoom == roomName && eventFlagData.SelfIntoro_All && !eventFlagData.SelfIntoro_Call)
            {
                Debug.Log("���m�N�}�̓o��");
                eventFlagData.SelfIntoro_Call = true;
                gameManager.OpenTextWindow(MonokumaCallText);
            }
        }

    }
}

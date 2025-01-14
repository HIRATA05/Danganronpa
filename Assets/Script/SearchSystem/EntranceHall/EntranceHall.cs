using System.Collections;
using System.Collections.Generic;
using TECHC.Kamiyashiki;
using UnityEngine;

public class EntranceHall : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;
    [SerializeField] private DialogueText EscepeText;
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
            //�n�߂Č��փz�[���֓��������̉�b
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_EntranceHall)
            {
                eventFlagData.RoomIn_EntranceHall = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //���ȏЉ��Ăяo��
            if (eventFlagData.currentRoom == roomName && eventFlagData.SelfIntoro_All && !eventFlagData.SelfIntoro_Call)
            {
                Debug.Log("���m�N�}�̓o��");
                eventFlagData.SelfIntoro_Call = true;
                gameManager.OpenTextWindow(MonokumaCallText);
            }
            //�E�o�X�C�b�`�����
            if (gameManager.eventFlagData.itemDataBase.truthBullets[8].getFlag && !gameManager.eventFlagData.EscepeEvent)
            {
                gameManager.eventFlagData.EscepeEvent = true;
                gameManager.OpenTextWindow(EscepeText);
            }

        }
    }
}

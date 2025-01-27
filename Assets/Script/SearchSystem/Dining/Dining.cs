using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dining : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;
    [SerializeField] private DialogueText DiscStartText;
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
            //�n�߂ĐH���֓��������̉�b
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_Dining)
            {
                Debug.Log("RoomIn_Dining");
                eventFlagData.RoomIn_Dining = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //���m�N�}�̈��ӂ����̉�b
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.AdventureStart && eventFlagData.IronBars && eventFlagData.WarehouseRequest && !eventFlagData.DiningDiscStart)
            {
                Debug.Log("WarehouseRequest");
                eventFlagData.DiningDiscStart = true;
                gameManager.OpenTextWindow(DiscStartText);
            }
            //���ȏЉ��Ăяo��
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.SelfIntoro_All && !eventFlagData.SelfIntoro_Call)
            {
                Debug.Log("���m�N�}�̓o��");
                eventFlagData.SelfIntoro_Call = true;
                gameManager.OpenTextWindow(MonokumaCallText);
            }
        }

    }
}

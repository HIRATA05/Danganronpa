using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;
    [SerializeField] private DialogueText DiscStartText;

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
            //�n�߂đq�ɂ֓��������̉�b
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_Dining)
            {
                eventFlagData.RoomIn_Dining = true;
                gameManager.OpenTextWindow(RoomText);
            }
            //���J���̗v��
            if (eventFlagData.currentRoom == roomName && eventFlagData.WarehouseRequest)
            {
                eventFlagData.DiningDiscStart = true;
                gameManager.OpenTextWindow(DiscStartText);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;
    [SerializeField] private DialogueText RequestText;

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
            if (/*eventFlagData.currentRoom == roomName &&*/ !eventFlagData.RoomIn_Warehouse)
            {
                eventFlagData.RoomIn_Warehouse = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
            //���J���̗v��
            if (/*eventFlagData.currentRoom == roomName &&*/ eventFlagData.WarehouseRequest && eventFlagData.IronBars && !eventFlagData.IronBarsOpenBefor)
            {
                eventFlagData.IronBarsOpenBefor = true;
                gameManager.OpenTextWindow(RequestText);
            }
        }

    }
}

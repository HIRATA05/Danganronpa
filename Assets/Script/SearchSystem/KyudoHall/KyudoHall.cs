using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyudoHall : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public string roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomFirstText;

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
            if (eventFlagData.currentRoom == roomName && !eventFlagData.RoomIn_KyudoHall)
            {
                eventFlagData.RoomIn_KyudoHall = true;
                gameManager.OpenTextWindow(RoomFirstText);
            }
        }
    }
}

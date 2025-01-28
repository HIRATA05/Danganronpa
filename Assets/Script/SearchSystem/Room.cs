using TECHC.Kamiyashiki;
using UnityEngine;

public class Room : MonoBehaviour
{
    //���݂��镔�����` �����̐������쐬

    public RoomName roomName;

    [SerializeField, Header("�����̒��ł̎���������b")] private DialogueText RoomText;

    private GameManager gameManager;


    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //���݂̕��������̕����Ńt���O��True�Ŕ����t���O�����������C�x���g����
        if (gameManager.eventFlagData.currentRoom == roomName && roomName == RoomName.TestRoom && gameManager.eventFlagData.RoomIn)
        {
            Debug.Log("RoomIn");
            gameManager.OpenTextWindow(RoomText);
        }
        /*
        else if (gameManager.eventFlagData.currentRoom == roomName && roomName == "ClassRoom_F1" && gameManager.eventFlagData.GameStart_ClassRoom)
        {
            Debug.Log("GameStart_ClassRoom");
            gameManager.OpenTextWindow(RoomText);
        }
        
        else if (gameManager.eventFlagData.currentRoom == roomName && roomName == "ClassRoom_F1" && gameManager.eventFlagData.GameStart_All)
        {
            Debug.Log("GameStart_All");
            gameManager.OpenTextWindow(RoomText);
        }
        */
    }

    //���݂̕��������̕����ɕύX
    public void cureentRoomChange()
    {
        gameManager.eventFlagData.currentRoom = roomName;
    }
}

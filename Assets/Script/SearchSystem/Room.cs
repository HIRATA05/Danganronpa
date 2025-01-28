using TECHC.Kamiyashiki;
using UnityEngine;

public class Room : MonoBehaviour
{
    //現在いる部屋を定義 部屋の数だけ作成

    public RoomName roomName;

    [SerializeField, Header("部屋の中での自動発生会話")] private DialogueText RoomText;

    private GameManager gameManager;


    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        //現在の部屋がこの部屋でフラグがTrueで発生フラグが立った時イベント発生
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

    //現在の部屋をこの部屋に変更
    public void cureentRoomChange()
    {
        gameManager.eventFlagData.currentRoom = roomName;
    }
}

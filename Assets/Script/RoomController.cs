using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TECHC.Kamiyashiki
{
    [System.Serializable]
    public enum RoomName
    {
        ControlRoom,
        ClassRoom,
        Garden,
    }

    [System.Serializable]
    public class Room
    {
        public RoomName roomName; // 部屋の名前 (enum 型に変更)
        public GameObject roomObject; // 部屋のオブジェクト
        public GameObject mapImage; // マップでの表示画像
    }

    public class RoomController : MonoBehaviour
    {
        // 部屋類
        public List<Room> roomList = new List<Room>();
        private Dictionary<RoomName, Vector3> roomPositions = new Dictionary<RoomName, Vector3>();

        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private GameObject playerObject;

        private void Awake()
        {
            foreach (var room in roomList)
            {
                //　初期設定
                room.roomObject.name = room.roomName.ToString();
                room.mapImage.name = room.roomName.ToString();

                room.mapImage.GetComponent<Button>().onClick.AddListener(BeginMoveRoom);

                if (!roomPositions.ContainsKey(room.roomName))
                {
                    roomPositions.Add(room.roomName, room.roomObject.transform.position);
                }
            }
        }

        public IEnumerator WaitAndMoveRoom(RoomName roomName, float waitTime, Transform target)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            MoveRoom(roomName, target);
        }

        public void MoveRoom(RoomName roomName, Transform target)
        {
            if (roomPositions.TryGetValue(roomName, out Vector3 position))
            {
                target.position = position;
            }
            else
            {
                Debug.LogWarning($"Room '{roomName}' does not exist!");
            }
        }

        public void BeginMoveRoom()
        {
            Debug.Log("map");
            foreach (RoomName room in Enum.GetValues(typeof(RoomName)))
            {
                if (eventSystem.currentSelectedGameObject.name == room.ToString())
                {
                    Debug.Log("move");
                    MoveRoom(room, playerObject.transform);
                }
            }
        }
    }
}